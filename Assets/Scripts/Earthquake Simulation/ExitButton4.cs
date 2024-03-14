using UnityEngine;
using UnityEngine.UI;

public class ExitButton4 : MonoBehaviour
{
    // 버튼 오브젝트를 연결할 수 있는 변수
    public Button exitButton4;

    void Start()
    {
        // 버튼이 클릭되었을 때 Quit 메소드 호출
        exitButton4.onClick.AddListener(Quit4);
    }

    void Quit4()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
}