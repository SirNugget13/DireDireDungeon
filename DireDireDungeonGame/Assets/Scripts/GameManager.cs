using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool keyGotten;
    public int coinCount;

    public GameObject PauseUI;
    public bool IsPaused = false;

    public GameObject canvasKey;
    public TextMeshProUGUI canvasCoinCount;

    public GameObject navMeshObject;
    public NavMeshPlus.Components.NavMeshSurface navSurface;

    public TMPro.TextMeshProUGUI resume;
    public TMPro.TextMeshProUGUI quit;
    public TMPro.TextMeshProUGUI inventory;
    public Image selector;
    public int pauseStage = 1;
    public int optionSelected = 1;

    public Vector3 offset;

    public bool isButtonReset;

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

        if (IsPaused)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (verticalInput > 0 && optionSelected > 1 && isButtonReset)
            {
                optionSelected--;
                isButtonReset = false;
            }

            if (verticalInput < 0 && optionSelected < 3 && isButtonReset)
            {
                optionSelected++;
                isButtonReset = false;
            }

            if (verticalInput == 0 && horizontalInput == 0)
            {
                isButtonReset = true;
            }

            if (Input.GetButtonDown("Attack"))
                Select();

            if (pauseStage == 1)
                MoveSelect();
        }

        if (keyGotten)
        {
            canvasKey.SetActive(true);
        }

        canvasCoinCount.text = " x " + coinCount;
    }

    void MoveSelect()
    {
        if (optionSelected == 1)
        {
            selector.rectTransform.position = resume.rectTransform.position + offset;
        }

        if (optionSelected == 2)
        {
            selector.rectTransform.position = quit.rectTransform.position + offset;
        }

        if (optionSelected == 3)
        {
            selector.rectTransform.position = inventory.rectTransform.position + offset;
        }
    }

    void Select()
    {
        if (pauseStage == 1 && optionSelected == 1)
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            IsPaused = false;
        }

        if (pauseStage == 1 && optionSelected == 2)
        {
            Application.Quit();
        }

        if (pauseStage == 1 && optionSelected == 3)
        {
            pauseStage = 2;
        }
    }

    public void GenerateNavMesh()
    {
        Debug.Log("yabob");
        navSurface.BuildNavMesh();
    }
}
