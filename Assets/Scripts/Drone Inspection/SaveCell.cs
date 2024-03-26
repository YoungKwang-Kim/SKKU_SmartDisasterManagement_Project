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

    // Damage Analyze 창
    public GameObject damageChartScreen;
    private GameObject canvas;
    private RawImage damageImage;
    private int damageImageIndex;

    private SaveImageAndScrollView menualScrollView;

    public enum DistanceType
    {
        Near,
        Far
    }

    public DistanceType distanceType { get; set; }


    private void Awake()
    {
        menualScrollView = GameObject.FindObjectOfType<SaveImageAndScrollView>();

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
        damageImageIndex = droneImageSaveInfo.ID;
        if (droneImageSaveInfo.Distance > 2)
        {
            distanceType = DistanceType.Far;
        }
        else
        {
            distanceType = DistanceType.Near;
        }
    }

    // 셀을 눌렀을 때 Damage Inspector 씬이 생성된다.
    public void ButtonListener()
    {
        Debug.Log(distanceType.ToString());
        // 리스트에서 이름이 "damageChartScreen" + damageImageIndex인 GameObject를 찾습니다.
        GameObject existingDamageChartScreen = menualScrollView._damageChartScreenLists.Find(obj => obj.name == "damageChartScreen" + damageImageIndex);

        if (existingDamageChartScreen != null)
        {
            existingDamageChartScreen.SetActive(true);
        }
        else
        {
            // 오브젝트가 존재하지 않을 때 새로 생성
            GameObject prefabInstance = Instantiate(damageChartScreen, canvas.transform);
            prefabInstance.name = "damageChartScreen" + damageImageIndex;
            Debug.Log(prefabInstance.name);

            // 추가된 부분: RawImage 설정
            damageImage = prefabInstance.GetComponentInChildren<RawImage>();
            damageImage.texture = droneImage.texture;

            // 리스트에 새로 생성한 GameObject를 추가합니다.
            menualScrollView._damageChartScreenLists.Add(prefabInstance);
        }
    }
}
