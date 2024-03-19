using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwtichButton : MonoBehaviour
{
    [SerializeField] RectTransform HandleRectTransform;
    [SerializeField] GameObject imageObject;
    [SerializeField] GameObject signObject;
    [SerializeField] GameObject cubeObject;
    
    [SerializeField] Color backgroundColorChange;
   

    Image backImage;
    Color backgroundColor;

    Toggle toggle;
    Vector2 handlePosition;

   
    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = HandleRectTransform.anchoredPosition;
        backImage = HandleRectTransform.parent.GetComponent<Image>();
       

        backgroundColor = backImage.color;
       
        cubeObject.SetActive(false);

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on)
    {
        if (on)
        {
            HandleRectTransform.anchoredPosition = handlePosition * -1;
            backImage.color = backgroundColorChange;
            imageObject.SetActive(false);
            signObject.SetActive(false);
            cubeObject.SetActive(true);


        }
        else
        {
            HandleRectTransform.anchoredPosition = handlePosition;
            backImage.color = backgroundColor;
            imageObject.SetActive(true);
            signObject.SetActive(true);
            cubeObject.SetActive(false);
          
        }
        //HandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;

        //backImage.color = on? backgroundColorChange : backgroundColor;

    }


   

    }
