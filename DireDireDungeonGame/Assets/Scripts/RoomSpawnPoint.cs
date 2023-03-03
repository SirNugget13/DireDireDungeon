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
    private bool spawned = false;

    private void Start()
    {
        RT = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        if(!spawned)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, RT.bottomRooms.Length);
                Instantiate(RT.bottomRooms[rand], transform.position, Quaternion.identity);
                //spawn a room with a bottom door
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, RT.topRooms.Length);
                Instantiate(RT.topRooms[rand], transform.position, Quaternion.identity);
                //spawn a room with a top door
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, RT.leftRooms.Length);
                Instantiate(RT.leftRooms[rand], transform.position, Quaternion.identity);
                //spawn a room with a left door
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, RT.rightRooms.Length);
                Instantiate(RT.rightRooms[rand], transform.position, Quaternion.identity);
                //spawn a room with a right door
            }

            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RoomSpawnPoint") && collision.GetComponent<RoomSpawnPoint>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
