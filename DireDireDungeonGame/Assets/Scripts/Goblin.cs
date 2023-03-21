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

    private State goblinState;
    private Rigidbody2D rb;
    private GameObject player;


    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        goblinState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(goblinState);
        Vector2 Distance = player.transform.position - transform.position;
        float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);

        if(TotalDistance < triggerDistance)
        {
            //Debug.Log("abo");
            //goblinState = State.Chase;
        }
        else
        {
            goblinState = State.Idle;
        }

        
    }

    private void FixedUpdate()
    {
        if(goblinState == State.Chase)
        {
            Vector2 playerDistance = player.transform.position - transform.position;
            //rb.velocity = (playerDistance.normalized) * speed;
            //rb.AddForce((player.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        }
        else
        {
            if(goblinState == State.Idle)
            {
                rb.velocity *= 1 / speed;
            }
        }
    }

}
