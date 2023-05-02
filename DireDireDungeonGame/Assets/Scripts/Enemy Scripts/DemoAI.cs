using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemoAI : MonoBehaviour
{
    public Vector3 target;
    private NavMeshAgent agent;
    private GameObject player;
    private RoomTemplates rt;
    public Transform curRoom;
    public int lastIndex;
    public float playerRange = 10;

    private Rigidbody2D rb;
    public string direction;
    private float TotalDistance;

    public enum State
    {
        Wander,
        Chase
    };

    private State state;

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
            agent.speed = 4;
            
            target = curRoom.position;
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }

        if(state == State.Chase)
        {
            agent.speed = 6;
            
            SetTargetPosition();
            SetAgentPosition();

            if(TotalDistance > 10)
            {
                state = State.Wander;
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
        if (collision.CompareTag("Player"))
        {
            state = State.Chase;
        }
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

    void SetTargetPosition()
    {
        target = player.transform.position;
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
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

}
