using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSlowdown : MonoBehaviour
{
    public float slowDown;
    private Rigidbody2D rb;
    private HoverWithRB hwrb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hwrb = gameObject.GetComponent<HoverWithRB>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity *= slowDown;

        this.Wait(0.2f, () =>
        {
            hwrb.enabled = true;
        });
    }
}
