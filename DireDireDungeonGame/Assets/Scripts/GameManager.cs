using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    public bool keyGotten;
    public int coinCount;
    public int potionCount;
    public int armorUpgrade;
    public int swordUpgrade;
    public int speedUpgrade;
    public int floor;
    public float speedTimer;

    public GameObject PauseUI;
    public bool IsPaused = false;

    public GameObject canvasKey;
    public TextMeshProUGUI canvasCoinCount;

    public GameObject navMeshObject;
    public NavMeshPlus.Components.NavMeshSurface navSurface;

    public GameObject pauseText;
    public GameObject inventoryUI;

    public TMPro.TextMeshProUGUI resume;
    public TMPro.TextMeshProUGUI quit;
    public TMPro.TextMeshProUGUI inventory;
    public TMPro.TextMeshProUGUI armorText;
    public TMPro.TextMeshProUGUI swordText;
    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI potionText;

    public Image potions;
    public Image back;

    public Image selector;
    public Image invSelector;

    public int pauseStage = 1;
    public int optionSelected = 1;

    public Vector3 offset;

    public bool isButtonReset;

    private void Start()
    {
        potionCount = PlayerPrefs.GetInt("potionCount", 0);
        player = GameObject.FindGameObjectWithTag("Player");
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetButtonDown("Attack") && scene == 0)
            SceneManager.LoadScene(1);

        if (Input.GetButtonDown("Pause") && scene != 0 && scene != 2)
        {
            if (IsPaused)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
                IsPaused = false;
                pauseStage = 1;
                optionSelected = 1;
                pauseText.SetActive(true);
                inventoryUI.SetActive(false);
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

            MoveSelect();
        }

        else
        {
            if (speedTimer >= 0)
                speedTimer -= Time.deltaTime;
            if (speedTimer < 0 && (player != null && player.GetComponent<PlayerController>().speed != 10))
                player.GetComponent<PlayerController>().speed = 10;
        }

        if (keyGotten)
        {
            canvasKey.SetActive(true);
        }

        if (scene != 0 && scene != 2)
        {
            canvasCoinCount.text = " x " + coinCount;
            potionText.text = "Amount: " + potionCount;
            armorText.text = "Armor: " + armorUpgrade;
            swordText.text = "Sword: " + swordUpgrade;
            speedText.text = "Speed: " + speedUpgrade;
}
    }

    void MoveSelect()
    {
        if (pauseStage == 1)
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

        if (pauseStage == 2)
        {
            if (optionSelected == 1)
            {
                invSelector.rectTransform.position = potions.rectTransform.position + offset;
            }

            if (optionSelected == 2)
            {
                invSelector.rectTransform.position = back.rectTransform.position + offset;
            }
        }
    }

    void Select()
    {
        if (pauseStage == 2 && optionSelected == 1 && potionCount > 0)
        {
            Debug.Log("Used Potion");
            Potion();
        }
        if (pauseStage == 1)
        {
            if (optionSelected == 1)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
                IsPaused = false;
                optionSelected = 1;
            }

            if (optionSelected == 2)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }

            if (optionSelected == 3)
            {
                pauseStage = 2;
                pauseText.SetActive(false);
                inventoryUI.SetActive(true);
                optionSelected = 1;
            }
        }
        if (pauseStage == 2 && optionSelected == 2)
        {
            pauseStage = 1;
            pauseText.SetActive(true);
            inventoryUI.SetActive(false);
            optionSelected = 1;
        }
    }

    void Potion()
    {
        int mystery = Random.Range(1, 13);
        if (mystery == 1)
        {
            Debug.Log("Slowness");
            speedTimer = 6;
            player.GetComponent<PlayerController>().speed = 5;
        }
        if (mystery == 2)
        {
            Debug.Log("Invulnerability");
        }
        if (mystery == 3)
        {
            Debug.Log("Change Color");
        }
        if (mystery == 4)
        {
            Debug.Log("Speed Boost");
            speedTimer = 6;
            player.GetComponent<PlayerController>().speed = 20;
        }
        if (mystery == 5)
        {
            Debug.Log("Next Floor");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (mystery == 6)
        {
            Debug.Log("Die");
        }
        if (mystery == 7)
        {
            Debug.Log("Big Bad Teleport");
            if (GameObject.Find("BigBad") != null)
                player.transform.position = GameObject.Find("BigBad").transform.position;
        }
        if (mystery == 8)
        {
            Debug.Log("Time Slowdown");
        }
        if (mystery == 9)
        {
            Debug.Log("Nothing");
        }
        if (mystery == 10)
        {
            Debug.Log("+99 Gold");
            coinCount += 99;
        }
        if (mystery == 11)
        {
            Debug.Log("+1 Gold");
            coinCount++;
        }
        if (mystery == 12)
        {
            Debug.Log("+2000 Gold");
            coinCount += 2000;
        }
        potionCount--;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Potions", potionCount);
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.SetInt("Armor", armorUpgrade);
        PlayerPrefs.SetInt("Sword", swordUpgrade);
        PlayerPrefs.SetInt("Speed", speedUpgrade);
        PlayerPrefs.SetInt("Floor", floor);
    }

    public void Load()
    {
        potionCount = PlayerPrefs.GetInt("Potions", 0);
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        armorUpgrade = PlayerPrefs.GetInt("Armor", 0);
        swordUpgrade = PlayerPrefs.GetInt("Sword", 0);
        speedUpgrade = PlayerPrefs.GetInt("Speed", 0);
        floor = PlayerPrefs.GetInt("Floor", 0);
    }

    public void GenerateNavMesh()
    {
        Debug.Log("yabob");
        navSurface.BuildNavMesh();
    }
}
