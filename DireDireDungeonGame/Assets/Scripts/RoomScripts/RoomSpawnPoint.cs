using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour
{    
    public int openingDirection;
    // 1 = needs a room with a bottom door
    // 2 = needs a room with a top door
    // 3 = needs a room with a left door
    // 4 = needs a room with a right door

    private RoomTemplates rt;
    private int rand;
    
    public bool spawned = false;

    public WaveStatus ws;

    /*public Transform WaveOne;
    public Transform WaveTwo;
    public Transform WaveThree;
    public Transform RemainingWaves;*/

    private void Start()
    {
        /*WaveOne = GameObject.Find("WaveOne").transform;
        WaveTwo = GameObject.Find("WaveTwo").transform;
        WaveThree = GameObject.Find("WaveThree").transform;
        RemainingWaves = GameObject.Find("RemainingWaves").transform;*/

        //ws = GameObject.FindGameObjectWithTag("WaveStatus").GetComponent<WaveStatus>();
        rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if(!spawned)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, rt.bottomRooms.Length);
                InstantiateToWave(rt.bottomRooms[rand], 1);
                //spawn a room with a bottom door
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, rt.topRooms.Length);
                InstantiateToWave(rt.topRooms[rand], 1);
                //spawn a room with a top door
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, rt.leftRooms.Length);
                InstantiateToWave(rt.leftRooms[rand], 1);
                //spawn a room with a left door
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, rt.rightRooms.Length);
                InstantiateToWave(rt.rightRooms[rand], 1);
                //spawn a room with a right door
            }

            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomSpawnPoint"))// && collision.GetComponent<RoomSpawnPoint>().spawned == true
        {
            if(collision.GetComponent<RoomSpawnPoint>().spawned == false && spawned == false)
            {
                Instantiate(rt.closedRoom, transform.position, Quaternion.identity);
            }

            spawned = true;
            Destroy(gameObject);
        }
    }


    void InstantiateToWave(GameObject room, int wave)
    {
        GameObject instRoom;

        if(wave == 1)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            //instRoom.transform.parent = rt.transform;
        } 
        else if(wave == 2)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            //instRoom.transform.parent = rt.transform;
        }
        else if(wave == 3)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            //instRoom.transform.parent = rt.transform;
        }
        else
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            //instRoom.transform.parent = rt.transform;
        }
    }
}
