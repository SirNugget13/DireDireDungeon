using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkeleton : MonoBehaviour
{
   public enum State
    {
        Idle,
        Shoot,
        ShootAndRun,
        Run
    }

    public GameObject parentPrefab;

    public float ShootRange;
    public float ShootAndRunRange;
    public float RunRange;

    private bool isRunning;

    public float speed = 4;
    public float slowdown = 0.7f;
    public GameObject enemyNotice;
    public float playerDistance;

    public NewSkeletonCombat skc;

    public GameObject coinSpawner;
    
    //public GameObject goblinBody;

    public State skeletonState;
    private Rigidbody2D rb;
    private GameObject player;
    private CircleCollider2D cc;

    private string direction;

    private Animator anim;
    
    private bool unotimes = false;
    private bool doMove = true;
    private bool isDead = false;

    private float idleMoveTime = 2;
    private float idleMoveCounter;
    private bool canIdleMove;
    private float originalSkcCooldown;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cc = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        skeletonState = State.Idle;
        enemyNotice.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
        originalSkcCooldown = skc.shootCoolDown;
    }

    
    /*if (!unotimes)
                {
                    enemyNotice.SetActive(true);

                    unotimes = true;
                    doMove = false;

                    this.Wait(0.5f, () =>
                    {
        enemyNotice.SetActive(false);
        doMove = true;
    });
                }
            }
            else
{
    goblinState = State.Idle;
    unotimes = false;
}*/




// Update is called once per frame
void Update()
    {
        if(!isDead)
        {
            //Debug.Log(goblinState);
            Vector2 Distance = player.transform.position - transform.position;
            float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);
            playerDistance = TotalDistance;

            Vector2 DirectionToGo;

            if (TotalDistance < RunRange)
            {
                enemyNoticeCheck(State.Run);

                //Goes in the opposite direction of the player faster than normal speed
                DirectionToGo = Distance.normalized * -1f * (speed * 1.3f);
            }
            else if(TotalDistance < ShootAndRunRange)
            {
                enemyNoticeCheck(State.ShootAndRun);

                DirectionToGo = Distance.normalized * -1f * (speed * 1f);
            } 
            else if(TotalDistance < ShootRange)
            {
                DirectionToGo = Vector2.zero;

                enemyNoticeCheck(State.Shoot);
 
            }
            else
            {
                skeletonState = State.Idle;
                isRunning = false;
                unotimes = false;

                if (canIdleMove == false)
                {
                    idleMoveCounter += Time.deltaTime;
                    if (idleMoveCounter > idleMoveTime)
                    {
                        canIdleMove = true;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {
            if (skeletonState == State.Shoot)
            {
                CalculateDirectionAndShoot();
                skc.shootCoolDown = originalSkcCooldown;
                rb.velocity *= slowdown;
            }
            else if(skeletonState == State.ShootAndRun)
            {
                rb.velocity *= slowdown;

                Vector2 playerDistance = player.transform.position - transform.position;
                rb.velocity = (playerDistance.normalized) * speed * -0.7f;//Skeleton runs away from the player at 70% max speed

                skc.shootCoolDown = originalSkcCooldown * 1.3f;//Shoots slower while running away
                CalculateDirectionAndShoot();

            }
            else if(skeletonState == State.Run)
            {
                Vector2 playerDistance = player.transform.position - transform.position;
                rb.velocity *= slowdown;
                rb.velocity = (playerDistance.normalized) * speed * -1f;//runs away from the player at max speed
            }
            else if(skeletonState == State.Idle)
            {
                if (canIdleMove)
                {
                    Vector2 randomVelo = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                    rb.velocity = randomVelo;

                    idleMoveCounter = 0;
                    canIdleMove = false;

                }
                else { rb.velocity *= slowdown; }
            }
        }

        directionCheck();
        skeletonTurn(direction);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
        {
            Die(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Die(collision);
        }
    }

    private void skeletonTurn(string direction)
    {
        if(isRunning)
        {
            if (direction == "Right" && !isDead) { direction = "Left"; }
            
            if (direction == "Left" && !isDead) { direction = "Right"; }

            if (direction == "Up" && !isDead) { direction = "Down"; }

            if (direction == "Down" && !isDead) { direction = "Up"; }
        }
        
        if(direction == "Right" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
        }

        if (direction == "Left" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
        }

        if (direction == "Up" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
        }

        if (direction == "Down" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
        }

    }

    private void directionCheck()
    {
        Vector2 playerDirection = player.transform.position - gameObject.transform.position;
        
            if (Mathf.Abs(playerDirection.x) > Mathf.Abs(playerDirection.y))
            {
                if (playerDirection.x > 0)
                {
                    direction = "Right";
                }

                if (playerDirection.x < 0)
                {
                    direction = "Left";
                }
            }
            else
            {
                if (playerDirection.y > 0)
                {
                    direction = "Up";
                }

                if (playerDirection.y < 0)
                {
                    direction = "Down";
                }
            }
    }

    private void CalculateDirectionAndShoot()
    {
        float a = player.transform.position.x - transform.position.x;//Get the distance on the x axis from the player
        float b = player.transform.position.y - transform.position.y;//Get the distance on the y axis from the player
        float c = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));//Get the hypotenuse of the two distances

        float bDIVc = b / c;//dividing the y distance by the hypotenuse

        float angleInRads = Mathf.Asin(bDIVc);//Using arcSin to get the angle to the player in radians
        float angleInDegrees = Mathf.Rad2Deg * angleInRads;//Converting to degrees

        if (a < 0)//Weird stuff to make it work correctly
        {
            angleInDegrees *= -1;
            angleInDegrees += 180;
        }

        skc.ShootBow(player.transform.position - transform.position, new Vector3(0, 0, angleInDegrees));//Uses the function in the skeleton combat to fire using the direction
    }

    void Die(Collider2D collision)
    {
        doMove = false;
        isDead = true;
        rb.velocity = Vector2.zero;
        skeletonState = State.Idle;

        Vector2 oppositeDirection = (transform.position) - collision.transform.position;
        rb.AddForce(
            (oppositeDirection.normalized + gameObject.GetComponent<Rigidbody2D>().velocity) * 1200,
            ForceMode2D.Impulse);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        this.Wait(0.2f, () =>
        {
            rb.velocity = Vector2.zero;

            cc.enabled = false;

            anim.SetTrigger("DoExplosion");

            this.Wait(1.2f, () =>
            {
                Instantiate(coinSpawner, transform.position, Quaternion.identity);
                Destroy(parentPrefab);
            });

        });
    }

    void Die(Collision2D collision)
    {
        doMove = false;
        isDead = true;
        rb.velocity = Vector2.zero;
        skeletonState = State.Idle;

        Vector2 oppositeDirection = (transform.position) - collision.transform.position;
        rb.AddForce(
            (oppositeDirection.normalized + gameObject.GetComponent<Rigidbody2D>().velocity) * 1200,
            ForceMode2D.Impulse);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        this.Wait(0.2f, () =>
        {
            rb.velocity = Vector2.zero;

            cc.enabled = false;

            anim.SetTrigger("DoExplosion");

            this.Wait(1.2f, () =>
            {
                Instantiate(coinSpawner, transform.position, Quaternion.identity);
                Destroy(parentPrefab);
            });

        });
    }

    private void enemyNoticeCheck(State state)
    {
        if (!unotimes)
        {
            enemyNotice.SetActive(true);

            unotimes = true;
            doMove = false;

            this.Wait(0.5f, () =>
            {
                enemyNotice.SetActive(false);
                doMove = true;

                skeletonState = state;
                isRunning = false;

            });
        }
        else
        {
            skeletonState = state;
            isRunning = false;
        }
    }
}
