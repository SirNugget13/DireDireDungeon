using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;

    public bool canDie = true;

    public AudioSource PlayerAudio;

    public Color orignalPlayerColor;
    public Color ArmorColor;

    public AudioClip Coins;
    public AudioClip DodgeRoll;
    public AudioClip PlayerHit;
    public AudioClip RightStep;
    public AudioClip LeftStep;
    public AudioClip SwordSwing;
    public AudioClip PotionDrink;

    public CapsuleCollider2D swordHitbox;
    public bool hasArmor;
    public bool hasSpeedBoots;
    public bool isDead;

    public AudioSource yo;

    public GameObject armorObject;

    public enum State
    {
        Normal,
        Rolling,
        Damaged,
        Invulerable,
        Stopped,
        Dead
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public GameManager gm;

    public float speed = 10;
    public float speedLimiter;
    public float inputHorizontal;
    public float inputVertical;
    public LayerMask InvulLayer;
    private float rollSpeed;
    public float rollForce;
    public float rollCooldown;

    public bool canRoll = true;
    private float canRollTimer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public State state;
    public Direction direction;

    // Vector2 movement;
    private bool isDashButtonDown;
    //private bool isRollButtonDown;
    private Vector3 moveDir;
    private Vector3 rollDir;
    private Color normColor;
    private BoxCollider2D bc;

    private bool coinSoundPlaying = false;

    private int stepCounter;
    public float stepTiming;
    private float stepTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        state = State.Normal;
        direction = Direction.Right;
        normColor = sr.color;
        isDead = false;
        ApplyUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasArmor)
        {
            normColor = ArmorColor;
        }
        else
        {
            normColor = orignalPlayerColor;
        }
        
        if (state != State.Stopped && state != State.Dead)
        {
            switch (state)
            {
                case State.Normal:

                    if (canRoll == false)
                    {
                        if (canRollTimer >= rollCooldown)
                        {
                            canRoll = true;
                        }
                        else
                        {
                            canRollTimer += Time.deltaTime;
                        }
                    }

                    sr.color = normColor;
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Arrow"), false);

                    inputHorizontal = Input.GetAxisRaw("Horizontal");
                    inputVertical = Input.GetAxisRaw("Vertical");

                    moveDir = new Vector3(inputHorizontal, inputVertical);

                    if (Input.GetButtonDown("Roll"))
                    {
                        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                        {
                            rollDir = moveDir;
                            rollSpeed = rollForce;
                            state = State.Rolling;
                            PlayerAudio.PlayOneShot(DodgeRoll);
                        }
                    }

                    SetDirection();

                    break;
                case State.Rolling:

                    if (canRoll)
                    {
                        sr.color = Color.yellow;

                        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
                        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Arrow"));

                        float rollSpeedDropMultiplier = 100f;
                        rollSpeed -= rollSpeedDropMultiplier * Time.deltaTime;


                        float rollSpeedMinimum = 2f;

                        if (rollSpeed < rollSpeedMinimum)
                        {
                            canRoll = false;
                            canRollTimer = 0;
                            state = State.Normal;
                        }
                    }
                    else
                    {
                        state = State.Normal;
                    }

                    break;

                case State.Damaged:

                    sr.color = Color.red;

                    break;

                case State.Invulerable:

                    inputHorizontal = Input.GetAxisRaw("Horizontal");
                    inputVertical = Input.GetAxisRaw("Vertical");

                    moveDir = new Vector3(inputHorizontal, inputVertical);

                    if (Input.GetButtonDown("Roll"))
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
            }
        }
    }

    private void FixedUpdate()
    {
        if(state != State.Stopped && state != State.Dead)
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

                        if (stepTimer >= stepTiming)
                        {
                            Walking();
                        }
                        else { stepTimer += Time.deltaTime; }

                        rb.velocity = moveDir * speed;
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, 0);
                    }

                    break;
                case State.Rolling:

                    rb.velocity = rollDir.normalized * rollSpeed;

                    break;

                case State.Damaged:

                    rb.velocity = Vector2.zero;

                    this.Wait(0.2f, () =>
                    {
                        state = State.Normal;
                    });

                    break;

                case State.Invulerable:

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

                    break;
            }
        }
    }

    private void Walking()
    {
        stepCounter++;
        stepTimer = 0;
        if(stepCounter % 2 == 0)
        {
            PlayerAudio.PlayOneShot(RightStep);
        }
        else
        {
            PlayerAudio.PlayOneShot(LeftStep);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("GoblinSword") || collision.CompareTag("BigBadSwing")) && state == State.Normal)
        {
            state = State.Damaged;
            //Debug.Log("Player Hit!");
            sr.color = Color.red;

            CheckDie();

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
            
            if(coinSoundPlaying)
            {
                this.Wait(0.6f, () =>
                {
                    coinSoundPlaying = false;
                });
            }

            if(coinSoundPlaying == false)
            {
                PlayerAudio.PlayOneShot(Coins);
            }

        }

        if (collision.gameObject.CompareTag("Medium Coin"))
        {
            gm.coinCount += 5;
            Destroy(collision.gameObject);
            if (coinSoundPlaying)
            {
                this.Wait(0.6f, () =>
                {
                    coinSoundPlaying = false;
                });
            }

            if (coinSoundPlaying == false)
            {
                PlayerAudio.PlayOneShot(Coins);
            }
        }

        if (collision.gameObject.CompareTag("Small Coin"))
        {
            gm.coinCount += 1;
            Destroy(collision.gameObject);
            if (coinSoundPlaying)
            {
                this.Wait(0.6f, () =>
                {
                    coinSoundPlaying = false;
                });
            }

            if (coinSoundPlaying == false)
            {
                PlayerAudio.PlayOneShot(Coins);
            }
        }

        if (collision.gameObject.CompareTag("Arrow") && state == State.Normal)
        {
            state = State.Damaged;
            Debug.Log("Player Hit!");
            sr.color = Color.red;

            CheckDie();
        }
    }

    public void CheckDie()
    {
        PlayerAudio.PlayOneShot(PlayerHit);

        if(hasArmor)
        {
            RemoveArmor();
            Invulerablity(2.5f);
        }
        else
        {
            if(canDie)
            {
                //Debug.Break();
                state = State.Dead;
                isDead = true;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                //play death animation
                //slow camera zoom out?
            }
        }
    }

    void RemoveArmor()
    {
        hasArmor = false;
        //play armor falling off animation
    }

    public void ApplyUpgrades()
    {
        if(hasSpeedBoots)
        {
            Debug.Log("Yup");
            speed = 14;
        }
    }

    public void Invulerablity(float InvulTime)
    {
        sr.color = Color.cyan;

        state = State.Invulerable;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("GoblinCombat"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Arrow"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("BigBadSword"));

        this.Wait(InvulTime, () =>
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("GoblinCombat"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Arrow"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("BigBadSword"), false);
            sr.color = normColor;
            state = State.Normal;
        });
    }

    public void SetDirection()
    {
        if(inputHorizontal > 0 && Time.timeScale == 1)
        {
            direction = Direction.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (inputHorizontal < 0 && Time.timeScale == 1)
        {
            direction = Direction.Left;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if(inputVertical > 0 && Time.timeScale == 1)
        {
            direction = Direction.Up;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (inputVertical < 0 && Time.timeScale == 1)
        {
            direction = Direction.Down;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        //Debug.Log(direction);
    }
}
