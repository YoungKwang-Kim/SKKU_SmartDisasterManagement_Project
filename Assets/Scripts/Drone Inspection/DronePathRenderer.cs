using UnityEngine;

public class DronePathRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] waypoints; // 드론이 이동할 waypoint들을 저장하는 배열

    void Update()
    {
        // 드론의 현재 위치를 저장하여 라인 렌더러에 전달
        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }

        // 투명도를 조절하여 사라지는 효과 추가
        float alpha = 1f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.startColor = new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, alpha);
            lineRenderer.endColor = new Color(lineRenderer.endColor.r, lineRenderer.endColor.g, lineRenderer.endColor.b, alpha);
            alpha -= 0.1f; // 투명도를 점차적으로 감소시켜 사라지는 효과 구현
        }
    }
}