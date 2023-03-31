using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool keyGotten;
    public int coinCount;

    public GameObject canvasKey;
    public TextMeshProUGUI canvasCoinCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyGotten)
        {
            canvasKey.SetActive(true);
        }

        canvasCoinCount.text = " x " + coinCount;
    }
}
