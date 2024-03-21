using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // 선을 그릴 LineRenderer 컴포넌트
    private LineRenderer lineRenderer;

    // 선의 시작점과 끝점을 저장할 변수
    private Vector3 startPoint;
    private Vector3 endPoint;

    void Start()
    {
        // LineRenderer 컴포넌트를 추가합니다.
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // 선의 두께 설정
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // 선 색상 설정
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // 점의 갯수 설정 (여기서는 시작점과 끝점을 설정할 것이므로 2로 설정)
        lineRenderer.positionCount = 2;

        // 초기화 상태에서는 선을 보이지 않도록 설정
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 선을 보이도록 설정
            lineRenderer.enabled = true;

            // 시작점을 마우스 클릭한 위치로 설정
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0; // z 값은 Canvas 평면에 고정되어야 합니다.

            // 선의 시작점 설정
            lineRenderer.SetPosition(0, startPoint);
        }

        // 마우스 왼쪽 버튼이 떼어졌을 때
        if (Input.GetMouseButtonUp(0))
        {
            // 끝점을 마우스 클릭한 위치로 설정
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0; // z 값은 Canvas 평면에 고정되어야 합니다.

            // 선의 끝점 설정
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}