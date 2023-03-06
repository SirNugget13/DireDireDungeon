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

    private RoomTemplates RT;
    private int rand;
    
    public bool spawned = false;

    public WaveStatus ws;

    public Transform WaveOne;
    public Transform WaveTwo;
    public Transform WaveThree;
    public Transform RemainingWaves;

    private void Start()
    {
        WaveOne = GameObject.Find("WaveOne").transform;
        WaveTwo = GameObject.Find("WaveTwo").transform;
        WaveThree = GameObject.Find("WaveThree").transform;
        RemainingWaves = GameObject.Find("RemainingWaves").transform;

        ws = GameObject.FindGameObjectWithTag("WaveStatus").GetComponent<WaveStatus>();
        RT = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if(!spawned)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, RT.bottomRooms.Length);
                InstantiateToWave(RT.bottomRooms[rand], ws.waveNum);
                //spawn a room with a bottom door
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, RT.topRooms.Length);
                InstantiateToWave(RT.topRooms[rand], ws.waveNum);
                //spawn a room with a top door
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, RT.leftRooms.Length);
                InstantiateToWave(RT.leftRooms[rand], ws.waveNum);
                //spawn a room with a left door
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, RT.rightRooms.Length);
                InstantiateToWave(RT.rightRooms[rand], ws.waveNum);
                //spawn a room with a right door
            }

            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomSpawnPoint"))// && collision.GetComponent<RoomSpawnPoint>().spawned == true
        {
            Destroy(gameObject);
        }
    }


    void InstantiateToWave(GameObject room, int wave)
    {
        GameObject instRoom;

        if(wave == 1)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            instRoom.transform.parent = WaveOne.transform;
        } 
        else if(wave == 2)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            instRoom.transform.parent = WaveTwo.transform;
        }
        else if(wave == 3)
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            instRoom.transform.parent = WaveThree.transform;
        }
        else
        {
            instRoom = Instantiate(room, transform.position, Quaternion.identity);
            instRoom.transform.parent = RemainingWaves.transform;
        }
    }
}
