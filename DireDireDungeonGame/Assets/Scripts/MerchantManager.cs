using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MerchantManager : MonoBehaviour
{
    public GameManager gm;

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
            swordIcon.rectTransform.position = firstOptionText.rectTransform.position + offset;

            merchantText.text = "Merchant:\nThis'll increase the length of ya sword!";
        }

        if(optionSelected == 2)
        {
            swordIcon.rectTransform.position = secondOptionText.rectTransform.position + offset;

            merchantText.text = "Merchant:\nYou'll be able to take an extra hit with this high quality armor!";
        }

        if(optionSelected == 3)
        {
            swordIcon.rectTransform.position = thirdOptionText.rectTransform.position + offset;

            merchantText.text = "Merchant:\nI found a bucket of this stuff on the 82nd floor, not sure what it does.";
        }

        if (optionSelected == 4)
        {
            swordIcon.rectTransform.position = fourthOptionText.rectTransform.position + offset;

            merchantText.text = "Merchant:\nThese magic timbs will increase your running speed.";
        }

        if (optionSelected == 5)
        {
            swordIcon.rectTransform.position = fifthOptionText.rectTransform.position + offset;

            merchantText.text = "Merchant:\nGood luck! You'll need it.";
        }
    }
    void Select()
    {
        if (optionSelected == 1 && gm.coinCount >= 40 && gm.swordUpgrade < 3)
        {
            Debug.Log("Sword Upgrade");
            gm.coinCount -= 40;
            gm.swordUpgrade++;
           // gm.Save();

        }
        if (optionSelected == 2 && gm.coinCount >= 30 && gm.armorUpgrade == 0)
        {
            Debug.Log("Armor Upgrade");
            gm.coinCount -= 30;
            gm.armorUpgrade++;
            //gm.Save();
        }
        if (optionSelected == 3 && gm.coinCount >= 100)
        {
            Debug.Log("Mystery Potion");
            gm.potionCount++;
            gm.coinCount -= 100;
            //gm.Save();
        }
        if (optionSelected == 4 && gm.coinCount >= 50 && gm.speedUpgrade == 0)
        {
            Debug.Log("Speed Boots");
            gm.coinCount -= 50;
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
