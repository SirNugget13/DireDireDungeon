using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkeleton : MonoBehaviour
{
   private enum State
    {
        Idle,
        Shoot,
        ShootAndRun,
        Run
    }

    public float ShootRange;
    public float ShootAndRunRange;
    public float RunRange;

    private bool isRunning;

    public float speed = 4;
    public float slowdown = 0.7f;
    public GameObject enemyNotice;
    public float playerDistance;
    
    //public GameObject goblinBody;

    private State skeletonState;
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



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cc = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        skeletonState = State.Idle;
        enemyNotice.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
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
                skeletonState = State.Run;
                isRunning = true;

                //Goes in the opposite direction of the player faster than normal speed
                DirectionToGo = Distance.normalized * -1f * (speed * 1.3f);
            }
            else if(TotalDistance < ShootAndRunRange)
            {
                skeletonState = State.ShootAndRun;
                isRunning = false;

                DirectionToGo = Distance.normalized * -1f * (speed * 1f);
            } 
            else if(TotalDistance < ShootRange)
            {
                skeletonState = State.Shoot;
                isRunning = false;

                DirectionToGo = Vector2.zero;
            }
            else
            {
                skeletonState = State.Idle;
                isRunning = false;

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
                
            }
            else
            {
                if (skeletonState == State.Idle)
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
        }

        directionCheck();
        skeletonTurn(direction);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
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
                    Destroy(enemyNotice);
                    Destroy(gameObject);
                });

            });

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
        

        //Debug.Log(direction);

    }
}
