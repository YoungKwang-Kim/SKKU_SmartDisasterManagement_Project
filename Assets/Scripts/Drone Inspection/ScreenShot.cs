using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    // ��ũ�� ĸ���ϴ� �޼���
    public Texture2D CaptureScreenshot(string path, Camera camera, List<DroneImageSaveInfo> _list, RenderTexture renderTexture)
    {
        // ��ũ���� �ʺ�� ���̸� �����´�.
        int resWidth = Screen.width;
        int resHeight = Screen.height;
        // ���� �ؽ��� ����
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        // ī�޶��� Ÿ�� �ؽ�ó ����
        camera.targetTexture = rt;
        // ĸ���� �̹����� ������ �ؽ��� ����
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        // ī�޶� ������
        camera.Render();
        // Ȱ��ȭ�� ���� �ؽ��� ����
        RenderTexture.active = rt;
        // �ȼ� �б�
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        // �̹��� ����
        screenShot.Apply();
        // �̹��� ���Ϸ� ����
        RestoreScreenShot(path, screenShot, _list.Count.ToString());

        // ���ī�޶��� Ÿ���ؽ��ĸ� �ʱ�ȭ�Ѵ�.
        camera.targetTexture = renderTexture;

        return screenShot;
    }

    // ĸ���� ��ũ���� ���Ϸ� �����ϴ� �޼���
    private void RestoreScreenShot(string path, Texture2D screenShot, string fileNumber)
    {
        // �̹����� ������ ������ ���� ��� ����
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }

        string fileName = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + fileNumber.ToString() + ".png";
        // PNG �������� ���ڵ�
        byte[] bytes = screenShot.EncodeToPNG();
        // ���Ϸ� ����
        File.WriteAllBytes(fileName, bytes);
    }
}
