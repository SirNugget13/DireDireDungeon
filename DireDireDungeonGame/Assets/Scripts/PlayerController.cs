using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;

    private enum State
    {
        Normal,
        Rolling,
    }

    public float speed = 1;
    public float speedLimiter;
    public float inputHorizontal;
    public float inputVertical;
    public float rollSpeed;

    public float dashForce = 20;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private State state;

    // Vector2 movement;
    private bool isDashButtonDown;
    //private bool isRollButtonDown;
    private Vector3 moveDir;
    private Vector3 rollDir;
    private Color normColor;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        state = State.Normal;
        normColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:

                sr.color = normColor;

                inputHorizontal = Input.GetAxisRaw("Horizontal");
                inputVertical = Input.GetAxisRaw("Vertical");

                moveDir = new Vector3(inputHorizontal, inputVertical);

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isDashButtonDown = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDir = moveDir;
                    rollSpeed = 40f;
                    state = State.Rolling;
                }

                break;
            case State.Rolling:
                sr.color = Color.yellow;


                Debug.Log(rollSpeed);

                float rollSpeedDropMultiplier = 100f;
                rollSpeed -= rollSpeedDropMultiplier * Time.deltaTime;
                Debug.Log(rollSpeed);

                float rollSpeedMinimum = 2f;

                if(rollSpeed < rollSpeedMinimum)
                {
                    Debug.Log(rollSpeed);
                    state = State.Normal;
                }

                break;
        }

        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:

                if (inputHorizontal != 0 || inputVertical != 0)
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

                if (isDashButtonDown)
                {
                    Dash();
                }

                break;
            case State.Rolling:

                rb.velocity = rollDir * rollSpeed;

                break;
        }
    }

    public void Dash()
    {
        Vector3 dashPosition = transform.position + moveDir * dashForce;

        RaycastHit2D dashRaycast = Physics2D.Raycast(transform.position, moveDir, dashForce, LayerMask);

        if (dashRaycast.collider != null)
        {
            dashPosition = dashRaycast.point;
        }

        rb.MovePosition(dashPosition);

        Debug.Log("Dash");
        isDashButtonDown = false;
    }

}
