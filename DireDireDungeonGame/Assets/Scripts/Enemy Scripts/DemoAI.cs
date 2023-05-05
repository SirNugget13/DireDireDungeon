using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemoAI : MonoBehaviour
{
    public Vector3 target;
    public NavMeshAgent agent;
    private GameObject player;
    private RoomTemplates rt;
    public Transform curRoom;
    public int lastIndex;
    public float playerRange = 10;
    public float loseThePlayerDistance = 40;
    public float swingAtThePlayerDistance = 5;
    public GameObject Sword;

    public LayerMask layerMask;

    public float chaseSpeed = 6;
    public float wanderSpeed = 4;

    public float swingCoolDown;
    private float swingTimeCounter;
    private bool canSwing;

    private Rigidbody2D rb;
    public string direction;
    private float TotalDistance;

    private bool unotimes;

    public enum State
    {
        Wander,
        Chase,
        Swing
    };

    public State state;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        state = State.Wander;

        SelectRandomRoom();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Distance = player.transform.position - transform.position;
        TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);

        if (TotalDistance < playerRange)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            if (Mathf.Abs(playerRB.velocity.x) > 1 || Mathf.Abs(playerRB.velocity.y) > 1)
            {
                state = State.Chase;
            }
        }

        if (state == State.Wander)
        {
            agent.speed = wanderSpeed;
            
            if(unotimes == false) { SelectRandomRoom(); unotimes = true; }
            
            target = curRoom.position;
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }

        if(state == State.Chase)
        {
            unotimes = false;
            
            agent.speed = chaseSpeed;

            TargetPlayer();

            if (TotalDistance > loseThePlayerDistance)
            {
                state = State.Wander;
            }
            else if(TotalDistance < 5)
            {
                Swing();
            }


        }

        if(state == State.Swing)
        {

        }

        if(canSwing == false)
        {
            swingTimeCounter += Time.deltaTime;

            if(swingTimeCounter >= swingCoolDown)
            {
                canSwing = true;
            }
        }
        
        directionCheck();
    }

    private void FixedUpdate()
    {
        BigBadTurn(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NoRoomSpawnPoint") && state == State.Wander)
        {
            Vector2 Distance = transform.position - collision.transform.position;
            float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);

            if (collision.transform.parent.gameObject == curRoom.gameObject && TotalDistance < 1)
            {
                //Debug.Log(collision.transform.parent.gameObject);
                SelectRandomRoom();
            }
        }

        if (collision.CompareTag("Player"))
        {
            //Debug.Log("HeyNow");
            SeenPlayerRaycast();
        }
    }

    void SelectRandomRoom()
    {
        int rand = Random.Range(0, rt.roomList.Count);

        //Debug.Log(rand == lastIndex);

        if(rand == lastIndex)
        {
            while(rand == lastIndex) { rand = Random.Range(0, rt.roomList.Count); }
        }

        lastIndex = rand;
        curRoom = rt.roomList[rand].transform;
    }

    void TargetPlayer()
    {
        target = player.transform.position;
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void SeenPlayerRaycast()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 40, layerMask);
        Debug.DrawRay(transform.position, player.transform.position-transform.position);

        if (rayHit.collider != null)
        {
            GameObject hitObj = rayHit.collider.gameObject;

            //Debug.Log(hitObj.name);

            if(hitObj.CompareTag("Player") && state == State.Wander)
            {
                //Debug.Log("Hit it");
                state = State.Chase;
            }
        }
    }

    private void directionCheck()
    {
        if (Mathf.Abs(agent.velocity.x) > 0 || Mathf.Abs(agent.velocity.y) > 0)
        {
            if (Mathf.Abs(agent.velocity.x) > Mathf.Abs(agent.velocity.y))
            {
                if (agent.velocity.x > 0)
                {
                    direction = "Right";
                }

                if (agent.velocity.x < 0)
                {
                    direction = "Left";
                }
            }
            else
            {
                if (agent.velocity.y > 0)
                {
                    direction = "Up";
                }

                if (agent.velocity.y < 0)
                {
                    direction = "Down";
                }
            }
        }
    }

    private void BigBadTurn(string direction)
    {
        int count = 0;

        if (direction == "Right")
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
            count++;
        }

        if (direction == "Left")
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
            count++;
        }

        if (direction == "Up")
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
            count++;
        }

        if (direction == "Down")
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
            count++;
        }

        //Debug.Log(count);

    }

    private void Swing()
    {
        Sword.SetActive(true);

        agent.isStopped = true;

        Vector3 rotation = new Vector3(0, 0, -60) + transform.rotation.eulerAngles;

        Sword.LeanRotate(rotation, 0.5f);

        this.Wait(0.5f, () =>
        {
            Sword.LeanRotate(rotation * -1, 0.5f);
            Sword.SetActive(false);
            agent.isStopped = false;
        });
    }

}
