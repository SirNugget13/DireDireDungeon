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
        Debug.Log(goblinState);
        Vector2 Distance = player.transform.position - transform.position;
        float TotalDistance = Mathf.Abs(Distance.x) + Mathf.Abs(Distance.y);
        
    }

}
