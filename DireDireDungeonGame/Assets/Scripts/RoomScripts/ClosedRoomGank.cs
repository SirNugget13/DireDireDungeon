using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedRoomGank : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoRoomSpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
