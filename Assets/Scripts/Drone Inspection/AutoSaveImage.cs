using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class AutoSaveImage : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // 자동으로 스크린샷을 한 이미지를 올릴 스크롤뷰입니다.
    [SerializeField]
    RecyclableScrollRect _AutoSave;
    // 캡쳐할 드론 카메라 화면입니다.
    [SerializeField]
    private Camera droneCamera;
    // 스크린캡쳐하고 LiveView 화면을 다시 드론 카메라로 바꿔줄 용도입니다.
    [SerializeField]
    private RenderTexture droneCameRenderTexture;

    // CaptureScreenshot 메서드를 사용하기 위해 capture라는 속성을 선언해줍니다.
    private ScreenShot capture = new ScreenShot();
    // 레이캐스트가 활성화되는 여부를 판별하는 변수입니다.
    private bool isRayEnabled = true;

    // 저장될 이미지의 경로
    private string path = Application.dataPath + "/ScreenShotImages/";
    // 자동으로 캡쳐될 이미지들의 리스트
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

    // 드론이 크랙을 찾아내면 스크린을 캡쳐하고 스크롤뷰에 추가되는 메서드
    public void AutoScreenShot()
    {
        // 드론이 탐지할 Ray를 시각화합니다.
        Debug.DrawRay(droneCamera.transform.position, droneCamera.transform.forward * 3f, Color.red);
        RaycastHit hit;
        // Raycast가 활성화되어있으면
        if (isRayEnabled )
        {
            // 드론 카메라 앞으로 3만큼의 레이를 쏴서 감지가 되면
            if (Physics.Raycast(droneCamera.transform.position, droneCamera.transform.forward, out hit, 3f))
            {
                // 레이를 쐈는데 맞은 대상의 태그가 "Crack"이라면
                if (hit.collider.CompareTag("Crack"))
                {
                    // 스크린을 캡쳐하고 그것을 리스트에 추가합니다.
                    DroneImageSaveInfo obj = new DroneImageSaveInfo();
                    obj.Number = _AutoSaveList.Count.ToString();
                    obj.droneImage = capture.CaptureScreenshot(path, droneCamera, _AutoSaveList, droneCameRenderTexture);
                    _AutoSaveList.Add(obj);

                    // 추가된 데이터를 스크롤뷰에 반영해서 다시 불러옵니다.
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