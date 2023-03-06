using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomToList : MonoBehaviour
{
    public RoomTemplates rt;
    
    // Start is called before the first frame update
    void Start()
    {
        rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rt.roomList.Add(gameObject);
    }

}
