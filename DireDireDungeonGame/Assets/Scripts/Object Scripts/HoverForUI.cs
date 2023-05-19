using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverForUI : MonoBehaviour
{
    private float totalTime;
    public float Amplification = 0.25f;
    private Vector3 initialPos;
    public float time = 1;

    void Start()
    {
        initialPos = gameObject.GetComponent<RectTransform>().position;
        totalTime = Random.Range(0f, time);
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        gameObject.GetComponent<RectTransform>().position = initialPos + new Vector3(0, Mathf.Sin(totalTime * 5f) * Amplification, 0);
    }

}
