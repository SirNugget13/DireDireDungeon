using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlusOffset : MonoBehaviour
{
    public Transform thingToFollow;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = thingToFollow.position + offset;   
    }
}
