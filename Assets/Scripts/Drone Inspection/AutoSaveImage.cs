using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.IO;

public class AutoSaveImage : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _AutoSave;

    [SerializeField]
    private Camera droneCamera;

    [SerializeField]
    private RenderTexture droneCameRenderTexture;

    // 스크린샷 메서드
    private ScreenShot capture = new ScreenShot();

    // 저장될 이미지의 경로
    private string path = Application.dataPath + "/ScreenShotImages/";
    private List<DroneImageSaveInfo> _AutoSaveList = new List<DroneImageSaveInfo>();

    private void Awake()
    {
        _AutoSave.DataSource = this;
    }

    private void Update()
    {
        AutoScreenshot();
    }

    // 드론이 크랙을 찾아내면 스크린을 캡쳐하고 스크롤뷰에 추가되는 메서드
    public void AutoScreenshot()
    {
        Debug.DrawRay(droneCamera.transform.position, droneCamera.transform.forward * 3f, Color.red);
        if (Physics.Raycast(droneCamera.transform.position, droneCamera.transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Crack"))
            {
                // 스크린을 캡쳐하고 그것을 리스트에 추가합니다.
                DroneImageSaveInfo obj = new DroneImageSaveInfo();
                obj.Number = _AutoSaveList.Count.ToString();
                obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _AutoSaveList, droneCameRenderTexture);
                _AutoSaveList.Add(obj);

                // 추가된 데이터를 스크롤뷰에 반영해서 다시 불러옵니다.
                _AutoSave.ReloadData();
            }
        }
    }



    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _AutoSaveList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as SaveCell;
        item.ConfigureCell(_AutoSaveList[index], index);
    }
    #endregion
}