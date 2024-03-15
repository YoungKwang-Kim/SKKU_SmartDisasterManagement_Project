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

    // ��ũ���� �޼���
    private ScreenShot capture = new ScreenShot();

    // ����� �̹����� ���
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

    // ����� ũ���� ã�Ƴ��� ��ũ���� ĸ���ϰ� ��ũ�Ѻ信 �߰��Ǵ� �޼���
    public void AutoScreenshot()
    {
        Debug.DrawRay(droneCamera.transform.position, droneCamera.transform.forward * 3f, Color.red);
        if (Physics.Raycast(droneCamera.transform.position, droneCamera.transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Crack"))
            {
                // ��ũ���� ĸ���ϰ� �װ��� ����Ʈ�� �߰��մϴ�.
                DroneImageSaveInfo obj = new DroneImageSaveInfo();
                obj.Number = _AutoSaveList.Count.ToString();
                obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _AutoSaveList, droneCameRenderTexture);
                _AutoSaveList.Add(obj);

                // �߰��� �����͸� ��ũ�Ѻ信 �ݿ��ؼ� �ٽ� �ҷ��ɴϴ�.
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