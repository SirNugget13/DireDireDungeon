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

    public GameObject portalPair;
    public Vector3 portalPositionOffset;

    public List<GameObject> roomList;

    //public int chance = 8;

    public List<GameObject> portalRoomList;

    private int numRooms = 0;
    private float timer;
    private bool checkTimer = true;
    private bool unoTimes = false;
    private GameObject firstRoom;
    private GameObject lastRoom;

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
                    if (portalRoomList.Count % 2 != 0) { portalRoomList.RemoveAt(portalRoomList.Count - 1); }
                    
                    for (int i = 0; i < portalRoomList.Count; i++)
                    {
                        int randID1 = Random.Range(0, portalRoomList.Count);
                        GameObject portalRoom1 = portalRoomList[randID1];
                        portalRoomList.RemoveAt(randID1);

                        int randID2 = Random.Range(0, portalRoomList.Count);
                        GameObject portalRoom2 = portalRoomList[randID2];
                        portalRoomList.RemoveAt(randID2);

                        GameObject portalPairObj = Instantiate(portalPair, new Vector2(200, 200), Quaternion.identity);
                        PortalPairScript pps = portalPairObj.GetComponent<PortalPairScript>();
                        
                        pps.portal1.gameObject.transform.position = portalRoom1.transform.position + portalPositionOffset;
                        pps.portal2.gameObject.transform.position = portalRoom2.transform.position + portalPositionOffset;
                    }

                    Instantiate(KongImage, roomList[roomList.Count - 1].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    Instantiate(keyGoblin, roomList[roomList.Count - 1].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    Instantiate(Stickman, roomList[0].transform.position, Quaternion.identity);
                    checkTimer = false;
                }
            }
        }
    }
}
