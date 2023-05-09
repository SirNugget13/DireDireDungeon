using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript2 : MonoBehaviour
{
    public PortalPairScript ppS;
    public bool portal2DoTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && portal2DoTeleport)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(ppS.portal1.transform.position);
            collision.gameObject.GetComponent<PlayerController>().Invulerablity(3);
            ppS.portal1.GetComponent<PortalScript1>().portal1DoTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portal2DoTeleport = true;
    }
}
