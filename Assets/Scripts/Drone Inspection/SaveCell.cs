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
        if (droneImageSaveInfo.Distance > 2)
        {
            distanceType = DistanceType.Far;
        }
        else
        {
            distanceType = DistanceType.Near;
        }
    }

    // ���� ������ �� Damage Inspector ���� �����ȴ�.
    public void ButtonListener()
    {
        Debug.Log(distanceType.ToString());
        // ����Ʈ���� �̸��� "damageChartScreen" + damageImageIndex�� GameObject�� ã���ϴ�.
        GameObject existingDamageChartScreen = menualScrollView._damageChartScreenLists.Find(obj => obj.name == "damageChartScreen" + damageImageIndex);

        if (existingDamageChartScreen != null)
        {
            existingDamageChartScreen.SetActive(true);
        }
        else
        {
            // ������Ʈ�� �������� ���� �� ���� ����
            GameObject prefabInstance = Instantiate(damageChartScreen, canvas.transform);
            prefabInstance.name = "damageChartScreen" + damageImageIndex;
            Debug.Log(prefabInstance.name);

            // �߰��� �κ�: RawImage ����
            damageImage = prefabInstance.GetComponentInChildren<RawImage>();
            damageImage.texture = droneImage.texture;

            // ����Ʈ�� ���� ������ GameObject�� �߰��մϴ�.
            menualScrollView._damageChartScreenLists.Add(prefabInstance);
        }
    }
}
