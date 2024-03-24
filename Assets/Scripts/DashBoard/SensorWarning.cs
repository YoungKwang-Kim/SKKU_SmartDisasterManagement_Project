using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class SensorWarning : MonoBehaviour
{
    // 다리 신호등
    [Header("다리 신호등(순서는 1.왼쪽기둥, 2.왼쪽도로, 3.오른쪽기둥, 4.오른쪽도로)")]
    public List<Image> RedLightImage;
    //public GameObject GreenlightImage1; // 거리 센서 경고 이미지
    //public GameObject GreenlightImage2; // 압력 센서 경고 이미지

    public TextMeshProUGUI leftDistanceSensor;  // 왼쪽 거리 센서
    public TextMeshProUGUI leftPressureSensor;  // 왼쪽 압력 센서
    public TextMeshProUGUI rightDistanceSensor;  // 오른쪽 거리 센서
    public TextMeshProUGUI rightPressureSensor;  // 오른쪽 압력 센서

    public float dangerDistanceValue;  // 거리 센서 위험 수치
    public float dangerPressureValue;  // 압력 센서 위험 수치

    void Update()
    {
        WarningLight(leftDistanceSensor, dangerDistanceValue, RedLightImage[0]);
        WarningLight(leftPressureSensor, dangerPressureValue, RedLightImage[1]);
        WarningLight(rightDistanceSensor, dangerDistanceValue, RedLightImage[2]);
        WarningLight(rightPressureSensor, dangerPressureValue, RedLightImage[3]);
    }

    // 센서 값에 따라 경고 불빛을 토글합니다.
    private void WarningLight(TextMeshProUGUI sensorValue, float dangerValue, Image warningLight)
    {
        float sensorFloatValue;
        if (float.TryParse(sensorValue.text, out sensorFloatValue))
        {
            //warningLight.gameObject.SetActive(sensorFloatValue >= dangerValue);
            bool isDangerous = sensorFloatValue >= dangerValue;
            warningLight.gameObject.SetActive(isDangerous);

            if (isDangerous)
            {
                 //알림 전송
                NotificationManager.SendAndroidNotification("경고!", warningLight.name + "에서 위험한 수치가 감지되었습니다.");
            }
        }
    }

    
}
