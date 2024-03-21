using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        Button[] backButton = GetComponentsInChildren<Button>();
        backButton[1].onClick.AddListener(DisableScreen);
    }

    private void DisableScreen()
    {
        gameObject.SetActive(false);
    }
}
