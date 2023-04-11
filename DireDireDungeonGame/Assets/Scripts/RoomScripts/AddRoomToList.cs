using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomToList : MonoBehaviour
{
    public RoomTemplates rt;
    public int chance = 5;


    // Start is called before the first frame update
    void Start()
    {
        rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rt.roomList.Add(gameObject);
        gameObject.transform.parent = rt.transform;

        int randomID = Random.Range(0, chance);

        if (randomID == 1)
        {
            rt.portalRoomList.Add(gameObject);
        }
    }

}
