using UnityEngine;
using UnityEngine.UI;

public class ExitButton4 : MonoBehaviour
{
    // ��ư ������Ʈ�� ������ �� �ִ� ����
    public Button exitButton4;

    void Start()
    {
        // ��ư�� Ŭ���Ǿ��� �� Quit �޼ҵ� ȣ��
        exitButton4.onClick.AddListener(Quit4);
    }

    void Quit4()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}