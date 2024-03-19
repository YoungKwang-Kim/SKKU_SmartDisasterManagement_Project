using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class AutoSaveImage : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // �ڵ����� ��ũ������ �� �̹����� �ø� ��ũ�Ѻ��Դϴ�.
    [SerializeField]
    RecyclableScrollRect _AutoSave;
    // ĸ���� ��� ī�޶� ȭ���Դϴ�.
    [SerializeField]
    private Camera droneCamera;
    // ��ũ��ĸ���ϰ� LiveView ȭ���� �ٽ� ��� ī�޶�� �ٲ��� �뵵�Դϴ�.
    [SerializeField]
    private RenderTexture droneCameRenderTexture;

    // CaptureScreenshot �޼��带 ����ϱ� ���� capture��� �Ӽ��� �������ݴϴ�.
    private ScreenShot capture = new ScreenShot();
    // ����ĳ��Ʈ�� Ȱ��ȭ�Ǵ� ���θ� �Ǻ��ϴ� �����Դϴ�.
    private bool isRayEnabled = true;

    // ����� �̹����� ���
    private string path = Application.dataPath + "/ScreenShotImages/";
    // �ڵ����� ĸ�ĵ� �̹������� ����Ʈ
    private List<DroneImageSaveInfo> _AutoSaveList = new List<DroneImageSaveInfo>();

    private void Awake()
    {
        _AutoSave.DataSource = this;
        Debug.Log(Vector3.forward);
    }

    private void Update()
    {
        AutoScreenShot();
    }

    // ����� ũ���� ã�Ƴ��� ��ũ���� ĸ���ϰ� ��ũ�Ѻ信 �߰��Ǵ� �޼���
    public void AutoScreenShot()
    {
        // ����� Ž���� Ray�� �ð�ȭ�մϴ�.
        Debug.DrawRay(droneCamera.transform.position, droneCamera.transform.forward * 3f, Color.red);
        RaycastHit hit;
        // Raycast�� Ȱ��ȭ�Ǿ�������
        if (isRayEnabled )
        {
            // ��� ī�޶� ������ 3��ŭ�� ���̸� ���� ������ �Ǹ�
            if (Physics.Raycast(droneCamera.transform.position, droneCamera.transform.forward, out hit, 3f))
            {
                // ���̸� ���µ� ���� ����� �±װ� "Crack"�̶��
                if (hit.collider.CompareTag("Crack"))
                {
                    // ��ũ���� ĸ���ϰ� �װ��� ����Ʈ�� �߰��մϴ�.
                    DroneImageSaveInfo obj = new DroneImageSaveInfo();
                    obj.Number = _AutoSaveList.Count.ToString();
                    obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _AutoSaveList, droneCameRenderTexture);
                    _AutoSaveList.Add(obj);

                    // �߰��� �����͸� ��ũ�Ѻ信 �ݿ��ؼ� �ٽ� �ҷ��ɴϴ�.
                    _AutoSave.ReloadData();

                    isRayEnabled = false;

                    StartCoroutine(WaitForRay(1.3f));
                }
            }
        }
    }

    IEnumerator WaitForRay(float duration)
    {
        yield return new WaitForSeconds(duration);

        isRayEnabled = true;
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