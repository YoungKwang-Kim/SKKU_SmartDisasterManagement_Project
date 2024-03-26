using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using System.Collections;

public class SensorWarning : MonoBehaviour
{
    public Text DronCheckText;
    private bool isBlinking;
   


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
        WarningDistanceLight(leftDistanceSensor, dangerDistanceValue, RedLightImage[0]);
        WarningLight(leftPressureSensor, dangerPressureValue, RedLightImage[1]);
        WarningDistanceLight(rightDistanceSensor, dangerDistanceValue, RedLightImage[2]);
        WarningLight(rightPressureSensor, dangerPressureValue, RedLightImage[3]);
    }

    // 센서 값에 따라 경고 불빛
    private void WarningDistanceLight(TextMeshProUGUI sensorValue, float dangerValue, Image warningLight)
    {
        float sensorFloatValue = float.Parse(sensorValue.text);
        bool isDangerous = sensorFloatValue < dangerValue;
        warningLight.gameObject.SetActive(isDangerous);


        if (isDangerous && !isBlinking)
        {
            StartCoroutine(BlinkDronCheckText());
           
        }
    }

    private void WarningLight(TextMeshProUGUI sensorValue, float dangerValue, Image warningLight)
    {
        float sensorFloatValue = float.Parse(sensorValue.text);
        bool isDangerous = sensorFloatValue > dangerValue;
        warningLight.gameObject.SetActive(isDangerous);


        if (isDangerous && !isBlinking)
        {
            StartCoroutine(BlinkDronCheckText());

        }
    }

    private IEnumerator BlinkDronCheckText()
    {
        isBlinking = true;
        for (int i = 0; i < 4; i++)
        {
            DronCheckText.enabled = !DronCheckText.enabled;
            yield return new WaitForSeconds(1f);
        }
        DronCheckText.enabled = true;
        isBlinking = false; //깜박임이 끝나면 다시 DronCheckText를 활성화하고 isBlinking을 false로 설정하여 깜박이는 중이 아님을 나타냅니다.
    }
}

//IEnumerator BlinkText()
//{
//    while(true)
//    {
//        DronCheckText.DOFade(0.0f, 1).SetLoops(-1, LoopType.Yoyo); //Yoyo옵션으로 깜박이기
//        yield return new WaitForSeconds(1);
//    }
//}

//// 알림을 전송하는 메서드(구현실패)
//private void SendNotification(string sensorName)
//{
//    string notificationTitle = "경고!";
//    string notificationMessage = sensorName + "에서 위험한 수치가 감지";

//    // 알림 생성
//    var notification = new AndroidNotification
//    {
//        Title = notificationTitle,
//        Text = notificationMessage,
//        FireTime = System.DateTime.Now.AddSeconds(1),
//        SmallIcon = "icon_0"
//    };

//    // 알림 전송
//    AndroidNotificationCenter.SendNotification(notification, "default_channel_id");
//}

