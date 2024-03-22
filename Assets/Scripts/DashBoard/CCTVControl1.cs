using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CCTVControl1 : MonoBehaviour
{
    public float turnSpeed = 20f; // 카메라 회전 속도
    public Button toggleButton; // UI Button
    public RawImage rawImage; // RawImage

    private bool isEnabled = false; // 스크립트 활성화 여부

    void Start()
    {
        // UI Button의 클릭 이벤트에 ToggleScript 함수 연결
        toggleButton.onClick.AddListener(ToggleScript);
        // 초기에는 rawImage를 비활성화
        rawImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // 스크립트가 활성화되어있는 경우에만 회전 로직 실행
        if (isEnabled)
        {
            // 방향키 입력 받기
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 카메라 회전 계산
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += horizontalInput * turnSpeed * Time.deltaTime;
            rotation.x -= verticalInput * turnSpeed * Time.deltaTime; // 상하 회전 추가
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    // 스크립트 활성화 또는 비활성화
    void ToggleScript()
    {
        isEnabled = !isEnabled;
        // 버튼이 눌려진 상태일 때만 rawImage를 활성화
        rawImage.gameObject.SetActive(isEnabled);
    }
}