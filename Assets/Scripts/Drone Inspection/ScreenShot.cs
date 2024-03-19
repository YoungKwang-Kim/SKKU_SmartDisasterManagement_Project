using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    // 스크린 캡쳐하는 메서드
    public Texture2D CaptureScreenshot(string path, Camera camera, List<DroneImageSaveInfo> _list, RenderTexture renderTexture)
    {
        // 스크린의 너비와 높이를 가져온다.
        int resWidth = Screen.width;
        int resHeight = Screen.height;
        // 렌더 텍스쳐 생성
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        // 카메라의 타겟 텍스처 설정
        camera.targetTexture = rt;
        // 캡쳐할 이미지를 저장할 텍스쳐 생성
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        // 카메라 렌더링
        camera.Render();
        // 활성화된 렌더 텍스쳐 설정
        RenderTexture.active = rt;
        // 픽셀 읽기
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        // 이미지 적용
        screenShot.Apply();
        // 이미지 파일로 저장
        RestoreScreenShot(path, screenShot, _list.Count.ToString());

        // 드론카메라의 타겟텍스쳐를 초기화한다.
        camera.targetTexture = renderTexture;

        return screenShot;
    }

    // 캡쳐한 스크린을 파일로 저장하는 메서드
    private void RestoreScreenShot(string path, Texture2D screenShot, string fileNumber)
    {
        // 이미지를 저장할 폴더가 없는 경우 생성
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }

        string fileName = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + fileNumber.ToString() + ".png";
        // PNG 형식으로 인코딩
        byte[] bytes = screenShot.EncodeToPNG();
        // 파일로 저장
        File.WriteAllBytes(fileName, bytes);
    }
}
