using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float speedLimiter;
    public float inputHorizontal;
    public float inputVertical;

    public float dashForce = 20;

    private Rigidbody2D rb;

    private Vector2 movement;
    private bool isDashButtonDown;
    private Vector3 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       inputHorizontal = Input.GetAxisRaw("Horizontal");
       inputVertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }

        moveDir = new Vector3(inputHorizontal, inputVertical);
    }

    private void FixedUpdate()
    {
        if(inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = moveDir * speed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        if(isDashButtonDown)
        {
            Vector3 dashPosition = transform.position + moveDir * dashForce;

            RaycastHit2D dashRaycast = Physics2D.Raycast(transform.position, moveDir, dashForce);

            if(dashRaycast.collider != null)
            {
                dashPosition = dashRaycast.point;
            }

            isDashButtonDown = false;
        }
    }

    public void Dash()
    {
        rb.AddForce(new Vector2(inputHorizontal, inputVertical) * dashForce, ForceMode2D.Impulse);
        Debug.Log("Dash");
    }

}
