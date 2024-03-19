using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.IO;

//Dummy Data model for demonstration
public struct DroneImageSaveInfo
{
    public string Number;
    public Texture2D droneImage;
}

public class SaveImageAndScrollView : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // �������� ��ũ������ �� �̹����� �ø� ��ũ�Ѻ��Դϴ�.
    [SerializeField]
    RecyclableScrollRect _MenualSave;
    // ĸ���� ��� ī�޶� ȭ���Դϴ�.
    [SerializeField]
    private Camera droneCamera;
    // ��ũ��ĸ���ϰ� LiveView ȭ���� �ٽ� ��� ī�޶�� �ٲ��� �뵵�Դϴ�.
    [SerializeField]
    private RenderTexture droneCameRenderTexture;

    // CaptureScreenshot �޼��带 ����ϱ� ���� capture��� �Ӽ��� �������ݴϴ�.
    private ScreenShot capture = new ScreenShot();

    // ����� �̹����� ���
    private string path = Application.dataPath + "/ScreenShotImages/";
    // �������� ĸ�ĵ� �̹������� ����Ʈ�Դϴ�.
    private List<DroneImageSaveInfo> _MenualSaveList = new List<DroneImageSaveInfo>();

    private void Awake()
    {
        _MenualSave.DataSource = this;
    }

    // ��ũ���� ��ư�� ������ ��ũ���� ĸ���ϰ� ��ũ�Ѻ信 �߰��Ǵ� �޼���
    public void OnClickMenualScreenshotButton()
    {
        // ��ũ���� ĸ���ϰ� �װ��� ����Ʈ�� �߰��մϴ�.
        DroneImageSaveInfo obj = new DroneImageSaveInfo();
        obj.Number = _MenualSaveList.Count.ToString();
        obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _MenualSaveList, droneCameRenderTexture);
        _MenualSaveList.Add(obj);

        // �߰��� �����͸� ��ũ�Ѻ信 �ݿ��ؼ� �ٽ� �ҷ��ɴϴ�.
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