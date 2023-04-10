using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverWithRB : MonoBehaviour
{
    private float totalTime;
    public float Amplification = 0.25f;
    private Vector3 initialPos;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        initialPos = rb.position;
        totalTime = Random.Range(0f, 1f);
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        rb.position = initialPos + new Vector3(0, Mathf.Sin(totalTime * 5f) * Amplification, 0);
    }
}
