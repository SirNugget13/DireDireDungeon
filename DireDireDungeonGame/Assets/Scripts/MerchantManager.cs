using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MerchantManager : MonoBehaviour
{
    public GameManager gm;

    public Image icon1;
    public Image icon2;
    public Image icon3;
    public Image icon4;
    public Image icon5;

    public TMPro.TextMeshProUGUI merchantText;
    public TMPro.TextMeshProUGUI firstOptionText;
    public TMPro.TextMeshProUGUI secondOptionText;
    public TMPro.TextMeshProUGUI thirdOptionText;
    public TMPro.TextMeshProUGUI fourthOptionText;
    public TMPro.TextMeshProUGUI fifthOptionText;

    public Image soldOut1;
    public Image soldOut2;
    public Image soldOut3;
    public Image soldOut4;

    public int swordCost;
    public int armorCost;
    public int potionCost;
    public int speedCost;

    public Image swordIcon;
    public int optionSelected = 1;

    public Vector3 offset;

    public bool isButtonReset;

    private SceneLoader sl;

    // Start is called before the first frame update
    void Start()
    {
        merchantText.text = "Merchant:\nWelcome to the shop! Buy my wares!";
        sl = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

        soldOut1.gameObject.SetActive(false);
        soldOut2.gameObject.SetActive(false);
        soldOut3.gameObject.SetActive(false);
        soldOut4.gameObject.SetActive(false);

        firstOptionText.text = "Sword Upgrade: " + swordCost + "g";
        secondOptionText.text = "Armor Upgrade: " + armorCost + "g";
        thirdOptionText.text = "Mystery Potion: " + potionCost + "g";
        fourthOptionText.text = "Speed Boots: " + speedCost + "g";

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSoldOut();
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        if(verticalInput > 0 && optionSelected > 1 && isButtonReset)
        {
            optionSelected--;
            isButtonReset = false;
        }

        if(verticalInput < 0 && optionSelected < 5 && isButtonReset)
        {
            optionSelected++;
            isButtonReset = false;
        }

        if(verticalInput == 0 && horizontalInput == 0)
        {
            isButtonReset = true;
        }
        
        if (Input.GetButtonDown("Attack"))
            Select();

        MoveIcon();
    }

    void MoveIcon()
    {
        if(optionSelected == 1)
        {
            swordIcon.rectTransform.position = icon1.rectTransform.position;

            merchantText.text = "Merchant:\nThis'll increase the length of ya sword!";
        }

        if(optionSelected == 2)
        {
            swordIcon.rectTransform.position = icon2.rectTransform.position;

            merchantText.text = "Merchant:\nYou'll be able to take an extra hit with this high quality armor!";
        }

        if(optionSelected == 3)
        {
            swordIcon.rectTransform.position = icon3.rectTransform.position;

            merchantText.text = "Merchant:\nI found a bucket of this stuff on the 82nd floor, not sure what it does.";
        }

        if (optionSelected == 4)
        {
            swordIcon.rectTransform.position = icon4.rectTransform.position;

            merchantText.text = "Merchant:\nThese magic timbs will increase your running speed.";
        }

        if (optionSelected == 5)
        {
            swordIcon.rectTransform.position = icon5.rectTransform.position;

            merchantText.text = "Merchant:\nGood luck! You'll need it.";
        }
    }
    void Select()
    {
        if (optionSelected == 1 && gm.coinCount >= swordCost && gm.swordUpgrade < 3)
        {
            Debug.Log("Sword Upgrade");
            gm.coinCount -= swordCost;
            gm.swordUpgrade++;
           // gm.Save();

        }
        if (optionSelected == 2 && gm.coinCount >= armorCost && gm.armorUpgrade == 0)
        {
            Debug.Log("Armor Upgrade");
            gm.coinCount -= armorCost;
            gm.armorUpgrade++;
            //gm.Save();
        }
        if (optionSelected == 3 && gm.coinCount >= potionCost)
        {
            Debug.Log("Mystery Potion");
            gm.potionCount++;
            gm.coinCount -= potionCost;
            //gm.Save();
        }
        if (optionSelected == 4 && gm.coinCount >= speedCost && gm.speedUpgrade == 0)
        {
            Debug.Log("Speed Boots");
            gm.coinCount -= speedCost;
            gm.speedUpgrade++;
            //gm.Save();
        }
        if (optionSelected == 5)
        {
            gm.Save();
            sl.LoadScenes(1);
        }
    }

    void CheckIfSoldOut()
    {
        if(gm.swordUpgrade == 3)
        {
            soldOut1.gameObject.SetActive(true);
        }

        if(gm.armorUpgrade == 1)
        {
            soldOut2.gameObject.SetActive(true);
        }

        if(gm.speedUpgrade == 1)
        {
            soldOut4.gameObject.SetActive(true);
        }
    }
}
