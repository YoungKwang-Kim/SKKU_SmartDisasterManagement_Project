using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class SensorWarning : MonoBehaviour
{
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
        WarningLight(leftDistanceSensor, dangerDistanceValue, RedLightImage[0]);
        WarningLight(leftPressureSensor, dangerPressureValue, RedLightImage[1]);
        WarningLight(rightDistanceSensor, dangerDistanceValue, RedLightImage[2]);
        WarningLight(rightPressureSensor, dangerPressureValue, RedLightImage[3]);
    }

    // ���� ���� ���� ��� �Һ��� ����մϴ�.
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
                 //�˸� ����
                NotificationManager.SendAndroidNotification("���!", warningLight.name + "���� ������ ��ġ�� �����Ǿ����ϴ�.");
            }
        }
    }

    
}
