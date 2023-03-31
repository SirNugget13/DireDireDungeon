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

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public GameManager gm;

    public float speed = 1;
    public float speedLimiter;
    public float inputHorizontal;
    public float inputVertical;
    private float rollSpeed;
    public float rollForce;

    public float dashForce = 20;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private State state;
    public Direction direction;

    // Vector2 movement;
    private bool isDashButtonDown;
    //private bool isRollButtonDown;
    private Vector3 moveDir;
    private Vector3 rollDir;
    private Color normColor;
    private BoxCollider2D bc;
    
    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        state = State.Normal;
        direction = Direction.Right;
        normColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                
                sr.color = normColor;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

                inputHorizontal = Input.GetAxisRaw("Horizontal");
                inputVertical = Input.GetAxisRaw("Vertical");

                moveDir = new Vector3(inputHorizontal, inputVertical);

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isDashButtonDown = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                    {
                        rollDir = moveDir;
                        rollSpeed = rollForce;
                        state = State.Rolling;
                    }
                }

                SetDirection();

                break;
            case State.Rolling:
                sr.color = Color.yellow;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));

                float rollSpeedDropMultiplier = 100f;
                rollSpeed -= rollSpeedDropMultiplier * Time.deltaTime;
                

                float rollSpeedMinimum = 2f;

                if(rollSpeed < rollSpeedMinimum)
                {
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

                rb.velocity = rollDir.normalized * rollSpeed;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("GoblinSword") && state == State.Normal)
        {
            Debug.Log("Goblin Killed You!");
        }

        if (collision.CompareTag("Key"))
        {
            gm.keyGotten = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Big Coin"))
        {
            gm.coinCount += 10;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Medium Coin"))
        {
            gm.coinCount += 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Small Coin"))
        {
            gm.coinCount += 1;
            Destroy(collision.gameObject);
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

    public void SetDirection()
    {
        if(inputHorizontal > 0)
        {
            direction = Direction.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (inputHorizontal < 0)
        {
            direction = Direction.Left;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if(inputVertical > 0)
        {
            direction = Direction.Up;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (inputVertical < 0)
        {
            direction = Direction.Down;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        //Debug.Log(direction);
    }
}
