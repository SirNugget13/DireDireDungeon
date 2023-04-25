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
    private string direction;

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
        float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);

        if (TotalDistance < playerRange)
        {
            state = State.Chase;
        }

        if (state == State.Wander)
        {
            target = curRoom.position;
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }

        if(state == State.Chase)
        {
            SetTargetPosition();
            SetAgentPosition();
        }

        directionCheck();
    }

    private void FixedUpdate()
    {
        BigBadTurn(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NoRoomSpawnPoint") && state == State.Wander)
        {
            //Debug.Log("Check");

            if(collision.transform.parent.gameObject == curRoom.gameObject)
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
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            {
                if (rb.velocity.x > 0)
                {
                    direction = "Right";
                }

                if (rb.velocity.x < 0)
                {
                    direction = "Left";
                }
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    direction = "Up";
                }

                if (rb.velocity.y < 0)
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
