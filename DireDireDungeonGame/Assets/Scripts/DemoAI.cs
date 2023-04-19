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
    public float playerRange;


    public enum State
    {
        Wander,
        Chase
    };

    private State state;

    // Start is called before the first frame update
    private void Start()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NoRoomSpawnPoint") && state == State.Wander)
        {
            if(collision.transform.parent.gameObject == curRoom)
            {
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
            while(rand == lastIndex)
            {
                int i = 0;
                Debug.Log(i);
                rand = Random.Range(0, rt.roomList.Count);
                i++;
            }
        }

        lastIndex = rand;

        //Debug.Log(rand);
        //Debug.Log(rt.roomList.Count);
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

}
