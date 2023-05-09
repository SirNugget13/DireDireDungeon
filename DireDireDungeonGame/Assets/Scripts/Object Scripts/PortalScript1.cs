using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript1 : MonoBehaviour
{
    public PortalPairScript ppS;
    public bool portal1DoTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && portal1DoTeleport)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(ppS.portal2.transform.position);
            collision.gameObject.GetComponent<PlayerController>().Invulerablity(3);
            ppS.portal2.GetComponent<PortalScript2>().portal2DoTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portal1DoTeleport = true;
    }
}
