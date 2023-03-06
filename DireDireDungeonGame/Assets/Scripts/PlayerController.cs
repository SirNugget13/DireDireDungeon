using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;

    public float dashForce = 20;

    private Rigidbody2D rb;

    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");

       movement.y = Input.GetAxisRaw("Vertical");
       
       
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (Input.GetButton("Dash"))
        {
            Dash();
        }
    }

    public void Dash()
    {
        rb.AddForce(movement * dashForce, ForceMode2D.Impulse);
        Debug.Log("Dash");
    }

}
