using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BridgeLight : MonoBehaviour
{
    // 다리 신호등
    [Header("다리 신호등(순서는 1.왼쪽기둥, 2.왼쪽도로, 3.오른쪽기둥, 4.오른쪽도로)")]
    public List<Image> GreenLight;
    public List<Image> YellowLight;
    public List<Image> RedLight;

    // 아두이노의 센서들
    [Header("아두이노 센서들")]
    [Tooltip("다리 왼쪽의 거리센서")]
    public TextMeshProUGUI leftDistanceSensor;
    [Tooltip("다리 왼쪽의 압력센서")]
    public TextMeshProUGUI leftPressureSensor;
    [Tooltip("다리 오른쪽의 거리센서")]
    public TextMeshProUGUI rightDistanceSensor;
    [Tooltip("다리 오른쪽의 압력센서")]
    public TextMeshProUGUI rightPressureSensor;

    [Header("위험수치들")]
    public float yellowDistanceValue;
    public float yellowPressureValue;
    public float redDistanceValue;
    public float redPressureValue;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var light in YellowLight)
        {
            light.enabled = false;
        }
        foreach (var light in RedLight)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RedLightState();
        YellowLightState();
    }
    
    private void YellowLightState()
    {
        float leftDistanceValue = float.Parse(leftDistanceSensor.text);
        float rightDistanceValue = float.Parse(rightDistanceSensor.text);
        float leftPressureValue = float.Parse(leftPressureSensor.text);
        float rightPressureValue = float.Parse(rightPressureSensor.text);

        // 왼쪽 기둥
        if (yellowPressureValue < leftPressureValue)
        {
            YellowLight[0].enabled = true;
        }
        else
        {
            YellowLight[0].enabled = false;
        }
        // 왼쪽 도로
        if (yellowDistanceValue < leftDistanceValue)
        {
            YellowLight[1].enabled = true;
        }
        else
        {
            YellowLight[1].enabled = false;
        }
        // 왼쪽 기둥
        if (yellowPressureValue < rightPressureValue)
        {
            YellowLight[2].enabled = true;
        }
        else
        {
            YellowLight[2].enabled = false;
        }
        // 오른쪽도로
        if (yellowDistanceValue < rightDistanceValue)
        {
            YellowLight[3].enabled = true;
        }
        else
        {
            YellowLight[3].enabled = false;
        }
    }
    
    private void RedLightState()
    {
        float leftDistanceValue = float.Parse(leftDistanceSensor.text);
        float rightDistanceValue = float.Parse(rightDistanceSensor.text);
        float leftPressureValue = float.Parse(leftPressureSensor.text);
        float rightPressureValue = float.Parse(rightPressureSensor.text);

        // 왼쪽 기둥
        if (redPressureValue < leftPressureValue)
        {
            RedLight[0].enabled = true;
        }
        else
        {
            RedLight[0].enabled = false;
        }
        // 왼쪽 도로
        if (redDistanceValue < leftDistanceValue)
        {
            RedLight[1].enabled = true;
        }
        else
        {
            RedLight[1].enabled = false;
        }
        // 왼쪽 기둥
        if (redPressureValue < rightPressureValue)
        {
            RedLight[2].enabled = true;
        }
        else
        {
            RedLight[2].enabled = false;
        }
        // 오른쪽도로
        if (redDistanceValue < rightDistanceValue)
        {
            RedLight[3].enabled = true;
        }
        else
        {
            RedLight[3].enabled = false;
        }
    }
}
