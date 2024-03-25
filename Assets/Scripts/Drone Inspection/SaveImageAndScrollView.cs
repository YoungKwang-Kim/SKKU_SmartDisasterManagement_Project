using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.IO;

//스크린샷 Cell에 들어갈 Data
public struct DroneImageSaveInfo
{
    public string Number;
    public Texture2D droneImage;
    public int ID;
}

// Damage Inspector 씬의 안에 들어갈 Data
public struct DamageChartInfo
{
    public Texture2D damageImage;
    public int ID;
}

public class SaveImageAndScrollView : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // 수동으로 스크린샷을 한 이미지를 올릴 스크롤뷰입니다.
    [SerializeField]
    RecyclableScrollRect _MenualSave;
    // 캡쳐할 드론 카메라 화면입니다.
    [SerializeField]
    private Camera droneCamera;
    // 스크린캡쳐하고 LiveView 화면을 다시 드론 카메라로 바꿔줄 용도입니다.
    [SerializeField]
    private RenderTexture droneCameRenderTexture;

    // CaptureScreenshot 메서드를 사용하기 위해 capture라는 속성을 선언해줍니다.
    private ScreenShot capture = new ScreenShot();

    
    public List<DamageChartInfo> _damageChartList { get; set; }

    

    // 저장될 이미지의 경로
    private string path = Application.dataPath + "/ScreenShotImages/";
    // 수동으로 캡쳐될 이미지들의 리스트입니다.
    private List<DroneImageSaveInfo> _MenualSaveList = new List<DroneImageSaveInfo>();

    private void Awake()
    {
        _MenualSave.DataSource = this;
        _damageChartList = new List<DamageChartInfo>();
    }

    // 스크린샷 버튼을 누르면 스크린을 캡쳐하고 스크롤뷰에 추가되는 메서드
    public void OnClickMenualScreenshotButton()
    {
        // 스크린을 캡쳐하고 그것을 리스트에 추가합니다.
        DroneImageSaveInfo obj = new DroneImageSaveInfo();
        DamageChartInfo damageImage = new DamageChartInfo();
        obj.Number = _MenualSaveList.Count.ToString();
        obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _MenualSaveList, droneCameRenderTexture);
        damageImage.damageImage = obj.droneImage;
        damageImage.ID = _damageChartList.Count;
        obj.ID = damageImage.ID;
        _damageChartList.Add(damageImage);
        _MenualSaveList.Add(obj);

        
        
        

        // 추가된 데이터를 스크롤뷰에 반영해서 다시 불러옵니다.
        _MenualSave.ReloadData();
    }

    

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _MenualSaveList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as SaveCell;
        item.ConfigureCell(_MenualSaveList[index], index);
    }
    #endregion
}