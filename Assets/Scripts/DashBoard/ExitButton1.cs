using UnityEngine;
using UnityEngine.UI;

public class ExitButton1 : MonoBehaviour
{
    // ��ư ������Ʈ�� ������ �� �ִ� ����
    public Button exitButton1;

    void Start()
    {
        // ��ư�� Ŭ���Ǿ��� �� Quit �޼ҵ� ȣ��
        exitButton1.onClick.AddListener(Quit1);
    }

    void Quit1()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}