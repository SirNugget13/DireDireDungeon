using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MerchantManager : MonoBehaviour
{
    public GameManager gm;

    public TMPro.TextMeshProUGUI toptext;
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
        toptext.text = "Merchant:\nWelcome to the shop! Buy my wares!";
        sl = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        if(optionSelected == 2)
        {
            swordIcon.rectTransform.position = secondOptionText.rectTransform.position + offset;
        }

        if(optionSelected == 3)
        {
            swordIcon.rectTransform.position = thirdOptionText.rectTransform.position + offset;
        }

        if (optionSelected == 4)
        {
            swordIcon.rectTransform.position = fourthOptionText.rectTransform.position + offset;
        }

        if (optionSelected == 5)
        {
            swordIcon.rectTransform.position = fifthOptionText.rectTransform.position + offset;
        }
    }
    void Select()
    {
        if (optionSelected == 1 && gm.coinCount >= 40)
        {
            Debug.Log("Sword Upgrade");
            gm.coinCount -= 40;
        }
        if (optionSelected == 2 && gm.coinCount >= 30)
        {
            Debug.Log("Armor Upgrade");
            gm.coinCount -= 30;
        }
        if (optionSelected == 3 && gm.coinCount >= 100)
        {
            Debug.Log("Mystery Potion");
            gm.potionCount++;
            gm.coinCount -= 100;
        }
        if (optionSelected == 4 && gm.coinCount >= 50)
        {
            Debug.Log("Speed Boots");
            gm.coinCount -= 50;
        }
        if (optionSelected == 5)
        {
            gm.Save();
            sl.LoadScenes(1);
        }
    }
}
