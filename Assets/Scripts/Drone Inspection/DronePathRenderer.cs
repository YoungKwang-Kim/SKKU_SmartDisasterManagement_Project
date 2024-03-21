using UnityEngine;

public class DronePathRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] waypoints; // 드론이 이동할 waypoint들을 저장하는 배열

    // 라인 렌더러가 사라지는 시간 (초)
    public float fadeDuration = 1.0f;

    // 시작 시간
    private float startTime;

    private void Start()
    {
        lineRenderer.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }

        // 시작 시간 초기화
        startTime = Time.time;
    }
}