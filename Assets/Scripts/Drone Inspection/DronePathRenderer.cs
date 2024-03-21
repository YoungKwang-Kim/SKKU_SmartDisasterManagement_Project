using UnityEngine;

public class DronePathRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] waypoints; // ����� �̵��� waypoint���� �����ϴ� �迭

    // ���� �������� ������� �ð� (��)
    public float fadeDuration = 1.0f;

    // ���� �ð�
    private float startTime;

    private void Start()
    {
        lineRenderer.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }

        // ���� �ð� �ʱ�ȭ
        startTime = Time.time;
    }
}