using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public RandomEnemySpawn res;
    public bool isPortalRoom;
    public int Chance = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        int randomID = Random.Range(0, Chance);

        if(randomID == 1)
        {
            //Spawn portal
            //Look for room without a portal
            //set portal in room
            //change that room's status to portaled
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
