using UnityEngine;
using UnityEngine.UI;

public class ExitButton3 : MonoBehaviour
{
    // ��ư ������Ʈ�� ������ �� �ִ� ����
    public Button exitButton3;

    void Start()
    {
        // ��ư�� Ŭ���Ǿ��� �� Quit �޼ҵ� ȣ��
        exitButton3.onClick.AddListener(Quit3);
    }

    void Quit3()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}