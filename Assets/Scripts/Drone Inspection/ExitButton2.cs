using UnityEngine;
using UnityEngine.UI;

public class ExitButton2 : MonoBehaviour
{
    // ��ư ������Ʈ�� ������ �� �ִ� ����
    public Button exitButton2;

    void Start()
    {
        // ��ư�� Ŭ���Ǿ��� �� Quit �޼ҵ� ȣ��
        exitButton2.onClick.AddListener(Quit2);
    }

    void Quit2()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}