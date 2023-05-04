using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
    public GameManager gm;

    public TMPro.TextMeshProUGUI toptext;
    public TMPro.TextMeshProUGUI firstOptionText;
    public TMPro.TextMeshProUGUI secondOptionText;
    public TMPro.TextMeshProUGUI thirdOptionText;
    public TMPro.TextMeshProUGUI fourthOptionText;
    public Image swordIcon;
    public int optionSelected = 1;

    public Vector3 offset;

    public bool isButtonReset;

    // Start is called before the first frame update
    void Start()
    {
        toptext.text = "ertbh";
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

        if(verticalInput < 0 && optionSelected < 4 && isButtonReset)
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
    }
}
