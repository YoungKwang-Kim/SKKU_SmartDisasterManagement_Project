using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveCell : MonoBehaviour, ICell
{
    // Damage Analyze 창
    public GameObject damageChartScreen;
    public GameObject damageChartCell;
    private GameObject canvas;

    // Damage Analyze 창을 키고 끄기
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
        // Cell을 누르면 Damage Analyze 씬으로 넘어갑니다.
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    // DataSource 안에 SetCell 메소드 안에서 호출됩니다.
    public void ConfigureCell(DroneImageSaveInfo droneImageSaveInfo, int cellIndex)
    {
        // Cell 안에 NumberLabel에 들어가는 번호가 1번부터 시작하도록 합니다.
        int labelIndex = int.Parse(droneImageSaveInfo.Number) + 1;
        _cellIndex = cellIndex;
        _droneImageSaveInfo = droneImageSaveInfo;
        numberLabel.text = labelIndex.ToString();
        droneImage.texture = droneImageSaveInfo.droneImage;
    }

    // 셀을 눌렀을 때 Damage Inspector 씬이 생성된다.
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
