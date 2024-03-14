using UnityEngine;
using UnityEngine.UI;

public class ExitButton3 : MonoBehaviour
{
    // 버튼 오브젝트를 연결할 수 있는 변수
    public Button exitButton3;

    void Start()
    {
        // 버튼이 클릭되었을 때 Quit 메소드 호출
        exitButton3.onClick.AddListener(Quit3);
    }

    void Quit3()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
}