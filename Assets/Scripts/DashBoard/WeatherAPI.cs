using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;  // Image�� ����ϱ� ���� �߰�

public class WeatherAPI : MonoBehaviour
{
    public Image weatherImage;  // SpriteRenderer ��� Image�� ���
    public Text temperatureText;

    void Start()
    {
        StartCoroutine(GetWeatherInfo());
    }

    IEnumerator GetWeatherInfo()
    {
        //Iat(�浵) = 37.5833, Ion(����) = 127
        //https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat=37.5833&lon=127&appid=9de2d70f99200d51e41cdc48f150976e";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                ProcessWeatherInfo(webRequest.downloadHandler.text);
            }
        }
    }

    public void ProcessWeatherInfo(string response)
    {
        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(response);

        //temp(�µ�) ������ kelvin
        float kelvinTemperature = weatherData.main.temp;

        // kelvin -> ����
        float celsiusTemperature = kelvinTemperature - 273.15f;

        temperatureText.text = celsiusTemperature.ToString("F0") + "��C";

        int weatherId = weatherData.weather[0].id;
        Sprite weatherSprite = GetWeatherIcon(weatherId);

        // Image�� sprite �Ӽ��� ���� �Ҵ�
        weatherImage.sprite = weatherSprite;
    }

    // ������ ����
    Sprite GetWeatherIcon(int weatherId)
    {
        string iconName = "";

        if (weatherId >= 200 && weatherId <= 299) iconName = "thunder"; // Thunderstorm
        else if (weatherId >= 300 && weatherId <= 399) iconName = "drizzle"; // Drizzle
        else if (weatherId >= 500 && weatherId <= 599) iconName = "Rain"; // Rain
        else if (weatherId >= 600 && weatherId <= 699) iconName = "snow"; // Snow
        else if (weatherId >= 700 && weatherId <= 799) iconName = "fog"; // fog
        else if (weatherId >= 801 && weatherId <= 899) iconName = "cloud"; // Clouds
        else iconName = "sun";

        // Resources ���� �Ʒ��� Icons �������� �̹����� �ҷ�����
        Sprite weatherSprite = Resources.Load<Sprite>("Icons/" + iconName);

        return weatherSprite;
    }

    [System.Serializable]
    public class WeatherData
    {
        public Main main;
        public Weather[] weather;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
    }

    [System.Serializable]
    public class Weather
    {
        public int id;
    }
}
