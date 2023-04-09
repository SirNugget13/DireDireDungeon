using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnZoneRadius : MonoBehaviour
{
    public float radius;
    private CircleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CircleCollider2D>();
        cc.radius = radius;
    }

}
