using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
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

        if(verticalInput < 0 && optionSelected < 3 && isButtonReset)
        {
            optionSelected++;
            isButtonReset = false;
        }

        if(verticalInput == 0 && horizontalInput == 0)
        {
            isButtonReset = true;
        }

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
    }

}
