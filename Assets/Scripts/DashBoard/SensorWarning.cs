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
   


    // �ٸ� ��ȣ��
    [Header("�ٸ� ��ȣ��(������ 1.���ʱ��, 2.���ʵ���, 3.�����ʱ��, 4.�����ʵ���)")]
    public List<Image> RedLightImage;
    //public GameObject GreenlightImage1; // �Ÿ� ���� ��� �̹���
    //public GameObject GreenlightImage2; // �з� ���� ��� �̹���

    public TextMeshProUGUI leftDistanceSensor;  // ���� �Ÿ� ����
    public TextMeshProUGUI leftPressureSensor;  // ���� �з� ����
    public TextMeshProUGUI rightDistanceSensor;  // ������ �Ÿ� ����
    public TextMeshProUGUI rightPressureSensor;  // ������ �з� ����

    public float dangerDistanceValue;  // �Ÿ� ���� ���� ��ġ
    public float dangerPressureValue;  // �з� ���� ���� ��ġ

  

    void Update()
    {
        WarningDistanceLight(leftDistanceSensor, dangerDistanceValue, RedLightImage[0]);
        WarningLight(leftPressureSensor, dangerPressureValue, RedLightImage[1]);
        WarningDistanceLight(rightDistanceSensor, dangerDistanceValue, RedLightImage[2]);
        WarningLight(rightPressureSensor, dangerPressureValue, RedLightImage[3]);
    }

    // ���� ���� ���� ��� �Һ�
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
        isBlinking = false; //�������� ������ �ٽ� DronCheckText�� Ȱ��ȭ�ϰ� isBlinking�� false�� �����Ͽ� �����̴� ���� �ƴ��� ��Ÿ���ϴ�.
    }
}

//IEnumerator BlinkText()
//{
//    while(true)
//    {
//        DronCheckText.DOFade(0.0f, 1).SetLoops(-1, LoopType.Yoyo); //Yoyo�ɼ����� �����̱�
//        yield return new WaitForSeconds(1);
//    }
//}

//// �˸��� �����ϴ� �޼���(��������)
//private void SendNotification(string sensorName)
//{
//    string notificationTitle = "���!";
//    string notificationMessage = sensorName + "���� ������ ��ġ�� ����";

//    // �˸� ����
//    var notification = new AndroidNotification
//    {
//        Title = notificationTitle,
//        Text = notificationMessage,
//        FireTime = System.DateTime.Now.AddSeconds(1),
//        SmallIcon = "icon_0"
//    };

//    // �˸� ����
//    AndroidNotificationCenter.SendNotification(notification, "default_channel_id");
//}

