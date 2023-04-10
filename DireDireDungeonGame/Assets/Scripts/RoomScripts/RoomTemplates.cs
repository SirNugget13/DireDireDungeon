using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public GameObject KongImage;
    public GameObject Stickman;
    public GameObject keyGoblin;

    public List<GameObject> roomList;

    private int numRooms = 0;
    private float timer;
    private bool checkTimer = true;

    private void Update()
    {
        if(checkTimer)
        {
            if (roomList.Count != numRooms)
            {
                timer = 0;
                numRooms = roomList.Count;
            }
            else
            {
                timer += Time.deltaTime;

                if (timer > 1)
                {
                    Instantiate(KongImage, roomList[roomList.Count - 1].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    Instantiate(keyGoblin, roomList[roomList.Count - 1].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    Instantiate(Stickman, roomList[0].transform.position, Quaternion.identity);
                    checkTimer = false;
                }
            }
        }
       
    }
}
