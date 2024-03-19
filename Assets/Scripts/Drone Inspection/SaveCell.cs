using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;


//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class SaveCell : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI numberLabel;
    public RawImage droneImage;

    //Model
    private DroneImageSaveInfo _droneImageSaveInfo;
    private int _cellIndex;

    private void Start()
    {
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

    // ���� ������ ��
    private void ButtonListener()
    {
        
    }
}
