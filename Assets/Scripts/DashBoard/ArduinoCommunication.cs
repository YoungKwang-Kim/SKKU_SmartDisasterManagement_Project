/*using UnityEngine;
using System.IO.Ports;

public class SensorDataReceiver : MonoBehaviour
{
    SerialPort serialPort;
    string portName = "COM3"; // 포트 이름 설정
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
        serialPort.ReadTimeout = 1000; // 읽기 타임아웃 설정
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

                    // 센서 데이터 처리
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
            //Debug.LogWarning("Failed to read sensor data from Arduino.");
        }
    }
}*/
using UnityEngine;
using System.IO.Ports;
using TMPro;

public class SensorDataReceiver : MonoBehaviour
{
    SerialPort serialPort;
    string portName = "COM3"; // 포트 이름 설정
    int baudRate = 9600;

    public TextMeshProUGUI distance1Text;
    public TextMeshProUGUI distance2Text;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI pressure1Text;
    public TextMeshProUGUI pressure2Text;

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
        serialPort.ReadTimeout = 1000; // 읽기 타임아웃 설정
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

                    // 센서 데이터를 TextMeshPro에 출력
                    distance1Text.text = "Distance Sensor " + distance1.ToString();
                    distance2Text.text = "Distance Sensor " + distance2.ToString();
                    temperatureText.text = "Temperature Sensor " + temperature.ToString();
                    pressure1Text.text = "Pressure Sensor "+ pressure1.ToString();
                    pressure2Text.text = "Pressure Sensor "+ pressure2.ToString();
                }
            }
        }
        catch (System.Exception)
        {
            //Debug.LogWarning("Failed to read sensor data from Arduino.");
        }
    }
}