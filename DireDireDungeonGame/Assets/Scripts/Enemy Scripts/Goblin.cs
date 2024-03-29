using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase
    }

    public float triggerDistance = 5;
    public float speed = 4;
    public float slowdown = 0.7f;
    public GameObject enemyNotice;
    public float playerDistance;


    //public GameObject goblinBody;

    private State goblinState;
    private Rigidbody2D rb;
    private GameObject player;
    private CircleCollider2D cc;


    private string direction;

    private Animator anim;
    
    private bool unotimes = false;
    private bool doMove = true;
    private bool isDead = false;


    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cc = gameObject.GetComponent<CircleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        goblinState = State.Idle;
        enemyNotice.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            //Debug.Log(goblinState);
            Vector2 Distance = player.transform.position - transform.position;
            float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);
            playerDistance = TotalDistance;

            if (TotalDistance < triggerDistance)
            {
                goblinState = State.Chase;

                if (!unotimes)
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
            }

        }
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {
            if (goblinState == State.Chase)
            {
                if (doMove)
                {
                    Vector2 playerDistance = player.transform.position - transform.position;
                    rb.velocity = (playerDistance.normalized) * speed;
                    //rb.AddForce((player.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (goblinState == State.Idle)
                {
                    rb.velocity *= slowdown;
                }
            }
        }

        directionCheck();
        goblinTurn(direction);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
        {
            doMove = false;
            isDead = true;
            rb.velocity = Vector2.zero;
            goblinState = State.Idle;

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

    private void goblinTurn(string direction)
    {
        int count = 0;
        
        if(direction == "Right" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
            count++;
        }

        if (direction == "Left" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
            count++;
        }

        if (direction == "Up" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 90));
            count++;
        }

        if (direction == "Down" && !isDead)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 270));
            count++;
        }

        //Debug.Log(count);

    }

    private void directionCheck()
    {
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            {
                if (rb.velocity.x > 0)
                {
                    direction = "Right";
                }

                if (rb.velocity.x < 0)
                {
                    direction = "Left";
                }
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    direction = "Up";
                }

                if (rb.velocity.y < 0)
                {
                    direction = "Down";
                }
            }
        }

        //Debug.Log(direction);

    }

}
