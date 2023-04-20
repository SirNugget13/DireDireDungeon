using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool keyGotten;
    public int coinCount;

    public GameObject PauseUI;
    private bool IsPaused = false;

    public GameObject canvasKey;
    public TextMeshProUGUI canvasCoinCount;

    public GameObject navMeshObject;
    public NavMeshPlus.Components.NavMeshSurface navSurface;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
                IsPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                PauseUI.SetActive(true);
                IsPaused = true;
            }
        }

        if (keyGotten)
        {
            canvasKey.SetActive(true);
        }

        canvasCoinCount.text = " x " + coinCount;
    }

    public void GenerateNavMesh()
    {
        navSurface.BuildNavMesh();
    }
}
