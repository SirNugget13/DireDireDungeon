using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public bool testfunction = false;
    
    private GameObject player;
    private PlayerController pc;

    public SceneLoader sl;

    public bool keyGotten;
    public int coinCount;
    public int potionCount;
    public int armorUpgrade;
    public int swordUpgrade;
    public int speedUpgrade;
    public int floor;

    public float speedEffectLength;
    public Vector3 bigBadTeleportOffset;

    private float speedTimer;

    public RoomTemplates rt;

    public GameObject PauseUI;
    public bool IsPaused = false;

    public GameObject canvasKey;
    public TextMeshProUGUI canvasCoinCount;

    public GameObject navMeshObject;
    public NavMeshPlus.Components.NavMeshSurface navSurface;

    public GameObject pauseText;
    public GameObject inventoryUI;

    public TMPro.TextMeshProUGUI highScore;
    public TMPro.TextMeshProUGUI resume;
    public TMPro.TextMeshProUGUI quit;
    public TMPro.TextMeshProUGUI inventory;
    public TMPro.TextMeshProUGUI armorText;
    public TMPro.TextMeshProUGUI swordText;
    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI potionText;
    public TMPro.TextMeshProUGUI FloorNumber;

    public Image potions;
    public Image back;

    public Image playerSprite;

    public Image selector;
    public Image invSelector;

    public int pauseStage = 1;
    public int optionSelected = 1;

    public Vector3 offset;

    public bool isButtonReset;

    private float setSpeed = 10;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>(); }

        sl = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
        potionCount = PlayerPrefs.GetInt("potionCount", 0);
        Load();

        UpgradeText();
       
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pc = player.GetComponent<PlayerController>();
            playerSprite.sprite = player.GetComponent<SpriteRenderer>().sprite;
            UpgradeApplier();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(testfunction)
        {
            BigBadTeleport();
            testfunction = false;
        }
        
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetButtonDown("Attack") && scene == 0)
        {
            sl.LoadScenes(1);
            //SceneManager.LoadScene(1);
            floor = 1;
            coinCount = 0;
            potionCount = 0;
            armorUpgrade = 0;
            speedUpgrade = 0;
            swordUpgrade = 0;

            PlayerPrefs.SetInt("Potions", potionCount);
            PlayerPrefs.SetInt("Coins", coinCount);
            PlayerPrefs.SetInt("Armor", armorUpgrade);
            PlayerPrefs.SetInt("Sword", swordUpgrade);
            PlayerPrefs.SetInt("Speed", speedUpgrade);
            PlayerPrefs.SetInt("Floor", floor);
        }
            

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            FloorNumber.text = "Floor: " + floor;
        }

        if (scene == 0)
            highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);

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
            if (speedTimer < 0 && (player != null && player.GetComponent<PlayerController>().speed != setSpeed))
                player.GetComponent<PlayerController>().speed = setSpeed;
        }

        if (keyGotten)
        {
            canvasKey.SetActive(true);
        }

        if(scene != 0) { canvasCoinCount.text = " x " + coinCount; }
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
                invSelector.rectTransform.sizeDelta = new Vector2(325, 375);
            }

            if (optionSelected == 2)
            {
                invSelector.rectTransform.position = back.rectTransform.position + offset;
                invSelector.rectTransform.sizeDelta = new Vector2(325, 100);
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
                Delete();
                sl.LoadScenes(0);
            }

            if (optionSelected == 3)
            {
                UpgradeText();
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
            speedTimer = speedEffectLength;
            player.GetComponent<PlayerController>().speed = 5;
        }
        if (mystery == 2)
        {
            Debug.Log("Invulnerability");
            pc.Invulerablity(120);
        }
        if (mystery == 3)
        {
            Debug.Log("Change Color");
            pc.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
        }
        if (mystery == 4)
        {
            Debug.Log("Speed Boost");
            speedTimer = speedEffectLength;
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
            pc.hasArmor = false;
            pc.CheckDie();
        }
        if (mystery == 7)
        {
            BigBadTeleport();
            pc.Invulerablity(3);
            //player.transform.position = GameObject.Find("BigBad").transform.position;
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

    public void BigBadTeleport()
    {
        Debug.Log("Big Bad Teleport");

        if (GameObject.Find("BigBad") != null)
        {
            GameObject bigbad = GameObject.Find("BigBad");

            GameObject selectedRoom = rt.roomList[Random.Range(0, rt.roomList.Count)];

            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bigbad.GetComponent<NavMeshAgent>().velocity = Vector2.zero;

            player.GetComponent<PlayerController>().Invulerablity(3);

            player.transform.position = selectedRoom.transform.position + bigBadTeleportOffset * -1;
            bigbad.GetComponent<NavMeshAgent>().Warp(selectedRoom.transform.position + bigBadTeleportOffset);

            bigbad.GetComponent<DemoAI>().direction = "Left";
        }
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

    public void Delete()
    {
        if (floor > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", floor);
        PlayerPrefs.SetInt("Potions", 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Armor", 0);
        PlayerPrefs.SetInt("Sword", 0);
        PlayerPrefs.SetInt("Speed", 0);
        PlayerPrefs.SetInt("Floor", 0);
    }

    public void UpgradeApplier()
    {
        if(armorUpgrade == 1)
        {
            Debug.Log(armorUpgrade);
            pc.hasArmor = true;
        }
            
        if(speedUpgrade == 1)
        {
            Debug.Log(speedUpgrade);
            pc.hasSpeedBoots = true;
            setSpeed = 14;
        }

        if(swordUpgrade > 0)
        {
            Debug.Log(swordUpgrade);

            if (swordUpgrade == 1)
            {
                pc.swordHitbox.offset = new Vector2(1.35f, 0);
                pc.swordHitbox.size = new Vector2(1.6f, 2.5f);
            }

            if (swordUpgrade == 2)
            {
                pc.swordHitbox.offset = new Vector2(1.5f, 0);
                pc.swordHitbox.size = new Vector2(2f, 2.5f);
            }

            if (swordUpgrade == 3)
            {
                pc.swordHitbox.offset = new Vector2(1.75f, 0);
                pc.swordHitbox.size = new Vector2(2.4f, 2.9f);
            }
        }
    }

    void UpgradeText()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            potionText.text = "Potions: " + potionCount;
            
            if (armorUpgrade == 1)
            {
                armorText.text = "Armor: Equipped";
            }
            else { armorText.text = "Armor: Not equipped"; }

            if (speedUpgrade == 1)
            {
                speedText.text = "Running Speed: 140%";
            }
            else { speedText.text = "Running Speed: 100%"; }

            if (swordUpgrade > 0)
            {
                if (swordUpgrade == 1)
                {
                    swordText.text = "Sword Reach: 133%";
                }

                if (swordUpgrade == 2)
                {
                    swordText.text = "Sword Reach: 166%";
                }

                if (swordUpgrade == 3)
                {
                    swordText.text = "Sword Reach: 200%";
                }
            }
            else { swordText.text = "Sword Reach: 100%"; }
        }
    }
    public void GenerateNavMesh()
    {
        //Debug.Log("yabob");
        navSurface.BuildNavMesh();
    }
}
