using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveCell : MonoBehaviour, ICell
{
    // Damage Analyze â
    public GameObject damageChartScreen;
    public GameObject damageChartCell;
    private GameObject canvas;

    // Damage Analyze â�� Ű�� ����
    bool isDamageChartOpen = false;

    //UI
    public TextMeshProUGUI numberLabel;
    public RawImage droneImage;

    //Model
    private DroneImageSaveInfo _droneImageSaveInfo;
    private int _cellIndex;

    private void Start()
    {
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
    }

    // ���� ������ �� Damage Inspector ���� �����ȴ�.
    private void ButtonListener()
    {
        if (GameObject.Find("damageChartScreen") != null && GameObject.Find("damageChartCell") != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDamageChartOpen = !isDamageChartOpen;
                damageChartScreen.SetActive(isDamageChartOpen);
                damageChartCell.SetActive(isDamageChartOpen);
            }
        }
        else
        {
            Instantiate(damageChartScreen, canvas.transform);
            Instantiate(damageChartCell, canvas.transform);
        }
    }
}
