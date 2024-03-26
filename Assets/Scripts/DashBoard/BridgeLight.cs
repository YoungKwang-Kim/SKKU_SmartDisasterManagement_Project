using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BridgeLight : MonoBehaviour
{
    // �ٸ� ��ȣ��
    [Header("�ٸ� ��ȣ��(������ 1.���ʱ��, 2.���ʵ���, 3.�����ʱ��, 4.�����ʵ���)")]
    public List<Image> GreenLight;
    public List<Image> YellowLight;
    public List<Image> RedLight;

    // �Ƶ��̳��� ������
    [Header("�Ƶ��̳� ������")]
    [Tooltip("�ٸ� ������ �Ÿ�����")]
    public TextMeshProUGUI leftDistanceSensor;
    [Tooltip("�ٸ� ������ �з¼���")]
    public TextMeshProUGUI leftPressureSensor;
    [Tooltip("�ٸ� �������� �Ÿ�����")]
    public TextMeshProUGUI rightDistanceSensor;
    [Tooltip("�ٸ� �������� �з¼���")]
    public TextMeshProUGUI rightPressureSensor;

    [Header("�����ġ��")]
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

        // ���� ���
        if (yellowPressureValue < leftPressureValue)
        {
            YellowLight[0].enabled = true;
        }
        else
        {
            YellowLight[0].enabled = false;
        }
        // ���� ����
        if (yellowDistanceValue < leftDistanceValue)
        {
            YellowLight[1].enabled = true;
        }
        else
        {
            YellowLight[1].enabled = false;
        }
        // ���� ���
        if (yellowPressureValue < rightPressureValue)
        {
            YellowLight[2].enabled = true;
        }
        else
        {
            YellowLight[2].enabled = false;
        }
        // �����ʵ���
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

        // ���� ���
        if (redPressureValue < leftPressureValue)
        {
            RedLight[0].enabled = true;
        }
        else
        {
            RedLight[0].enabled = false;
        }
        // ���� ����
        if (redDistanceValue < leftDistanceValue)
        {
            RedLight[1].enabled = true;
        }
        else
        {
            RedLight[1].enabled = false;
        }
        // ���� ���
        if (redPressureValue < rightPressureValue)
        {
            RedLight[2].enabled = true;
        }
        else
        {
            RedLight[2].enabled = false;
        }
        // �����ʵ���
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
