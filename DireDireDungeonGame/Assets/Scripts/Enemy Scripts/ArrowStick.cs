using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStick : MonoBehaviour
{
    public GameObject wholeArrow;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = wholeArrow.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            this.Wait(5f, () =>
            {
                Destroy(wholeArrow);
            });
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(wholeArrow);
        }

    }
}
