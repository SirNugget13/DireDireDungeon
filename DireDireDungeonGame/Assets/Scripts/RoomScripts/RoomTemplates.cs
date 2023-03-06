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

    public List<GameObject> roomList;

    private void Start()
    {
        this.Wait(3f, () =>
        {
            Instantiate(KongImage, roomList[roomList.Count - 1].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Instantiate(Stickman, roomList[0].transform.position, Quaternion.identity);
        });
    }
}
