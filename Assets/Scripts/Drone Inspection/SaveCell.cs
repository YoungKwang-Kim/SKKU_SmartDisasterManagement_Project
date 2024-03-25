using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveCell : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI numberLabel;
    public RawImage droneImage;

    //Model
    private DroneImageSaveInfo _droneImageSaveInfo;
    private int _cellIndex;

    // Damage Analyze â
    public GameObject damageChartScreen;
    private GameObject canvas;
    private RawImage damageImage;

    private int damageImageIndex;

    private SaveImageAndScrollView menualScrollView;

    private void Start()
    {
        menualScrollView = GameObject.FindObjectOfType<SaveImageAndScrollView>();

        canvas = GameObject.Find("Canvas");
        // Cell�� ������ Damage Analyze ������ �Ѿ�ϴ�.
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    // DataSource �ȿ� SetCell �޼ҵ� �ȿ��� ȣ��˴ϴ�.
    public void ConfigureCell(DroneImageSaveInfo droneImageSaveInfo, int cellIndex)
    {
        // Cell �ȿ� NumberLabel�� ���� ��ȣ�� 1������ �����ϵ��� �մϴ�.
        int labelIndex = int.Parse(droneImageSaveInfo.Number) + 1;
        _cellIndex = cellIndex;
        _droneImageSaveInfo = droneImageSaveInfo;
        numberLabel.text = labelIndex.ToString();
        droneImage.texture = droneImageSaveInfo.droneImage;
        damageImageIndex = droneImageSaveInfo.ID;

    }

    // ���� ������ �� Damage Inspector ���� �����ȴ�.
    private void ButtonListener()
    {

        if (GameObject.Find("damageChartScreen" + numberLabel.text) != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                damageChartScreen = GameObject.Find("damageChartScreen" + numberLabel.text);
                damageChartScreen.SetActive(true);
            }
        }
        else
        {
            int damageIndex = int.Parse(numberLabel.text) - 1;
            GameObject prefabInstance = Instantiate(damageChartScreen, canvas.transform);
            damageImage = prefabInstance.GetComponentInChildren<RawImage>();
            prefabInstance.name = "damageChartScreen" + numberLabel.text;

            damageImage.texture = droneImage.texture;
        }
    }
}
