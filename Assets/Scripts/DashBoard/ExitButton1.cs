using UnityEngine;
using UnityEngine.UI;

public class ExitButton1 : MonoBehaviour
{
    // 버튼 오브젝트를 연결할 수 있는 변수
    public Button exitButton1;

    void Start()
    {
        // 버튼이 클릭되었을 때 Quit 메소드 호출
        exitButton1.onClick.AddListener(Quit1);
    }

    void Quit1()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
}