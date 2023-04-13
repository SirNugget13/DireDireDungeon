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
    public GameObject endroomPortal;

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
                    firstRoom = portalRoomList[0];
                    lastRoom = portalRoomList[portalRoomList.Count - 1];
                    
                    if (portalRoomList.Count % 2 != 0) { portalRoomList.RemoveAt(portalRoomList.Count - 1); }
                    
                    for (int i = 0; i < portalRoomList.Count; i++)
                    {
                        if(portalRoomList[i] == firstRoom || portalRoomList[i] == lastRoom) { portalRoomList.RemoveAt(i); continue; }//Removes the first and last room from the portal room list
                        
                        //Assign a random room to hold the first portal and then remove it from the list
                        int randID1 = Random.Range(0, portalRoomList.Count);
                        GameObject portalRoom1 = portalRoomList[randID1];
                        portalRoomList.RemoveAt(randID1);

                        //Assign a random room to hold the second portal and then remove it from the list
                        int randID2 = Random.Range(0, portalRoomList.Count);
                        GameObject portalRoom2 = portalRoomList[randID2];
                        portalRoomList.RemoveAt(randID2);

                        //create the portal pair which contains both the first and second portal
                        GameObject portalPairObj = Instantiate(portalPair, new Vector2(200, 200), Quaternion.identity);
                        PortalPairScript pps = portalPairObj.GetComponent<PortalPairScript>();
                        
                        //Put the portals in the assigned rooms
                        pps.portal1.gameObject.transform.position = portalRoom1.transform.position + portalPositionOffset;
                        pps.portal2.gameObject.transform.position = portalRoom2.transform.position + portalPositionOffset;
                    }

                    //Instantiate(KongImage, lastRoom.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    Instantiate(keyGoblin, lastRoom.transform.position, Quaternion.identity);
                    Instantiate(endroomPortal, lastRoom.transform.position + new Vector3(-5, -5, 0), Quaternion.identity);
                    //Instantiate(Stickman, firstRoom.transform.position, Quaternion.identity);
                    checkTimer = false;
                }
            }
        }
    }
}
