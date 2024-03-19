using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SignSwtichButton : MonoBehaviour
{
    [SerializeField] RectTransform HandleRectTransform;
    //[SerializeField] GameObject imageObject;
    [SerializeField] GameObject wordSignObject;
    //[SerializeField] GameObject cubeObject;

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

        //cubeObject.SetActive(false);

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
            //imageObject.SetActive(false);
            wordSignObject.SetActive(false);
            //cubeObject.SetActive(true);


        }
        else
        {
            HandleRectTransform.anchoredPosition = handlePosition;
            backImage.color = backgroundColor;
            // imageObject.SetActive(true);
            wordSignObject.SetActive(true);
            //cubeObject.SetActive(false);

        }
     

    }




}
