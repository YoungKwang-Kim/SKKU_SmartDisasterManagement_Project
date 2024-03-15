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
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(DroneImageSaveInfo droneImageSaveInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _droneImageSaveInfo = droneImageSaveInfo;
        numberLabel.text = droneImageSaveInfo.Number;
        droneImage.texture = droneImageSaveInfo.droneImage;
    }

    // ¼¿À» ´­·¶À» ¶§
    private void ButtonListener()
    {
        
    }
}
