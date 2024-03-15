using UnityEngine;
using System.IO.Ports;

public class SensorDataReceiver : MonoBehaviour
{
    SerialPort serialPort;
    string portName = "COM3"; // ��Ʈ �̸� ����
    int baudRate = 9600;

    void Start()
    {
        OpenSerialPort();
    }

    void Update()
    {
        ReadSerialData();
    }

    void OnDestroy()
    {
        CloseSerialPort();
    }

    void OpenSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Failed to open serial port: " + portName);
        }
        serialPort.ReadTimeout = 1000; // �б� Ÿ�Ӿƿ� ����
    }

    void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    void ReadSerialData()
    {
        try
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                string data = serialPort.ReadLine();
                string[] sensorData = data.Split(',');
                if (sensorData.Length >= 5)
                {
                    float distance1 = float.Parse(sensorData[0]);
                    float distance2 = float.Parse(sensorData[1]);
                    float temperature = float.Parse(sensorData[2]);
                    int pressure1 = int.Parse(sensorData[3]);
                    int pressure2 = int.Parse(sensorData[4]);

                    // ���� ������ ó��
                    Debug.Log("Distance1: " + distance1);
                    Debug.Log("Distance2: " + distance2);
                    Debug.Log("Temperature: " + temperature);
                    Debug.Log("Pressure1: " + pressure1);
                    Debug.Log("Pressure2: " + pressure2);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Failed to read sensor data from Arduino.");
        }
    }
}