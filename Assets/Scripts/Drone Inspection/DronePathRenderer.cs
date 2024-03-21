using UnityEngine;

public class DronePathRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] waypoints; // ����� �̵��� waypoint���� �����ϴ� �迭

    void Update()
    {
        // ����� ���� ��ġ�� �����Ͽ� ���� �������� ����
        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }

        // ������ �����Ͽ� ������� ȿ�� �߰�
        float alpha = 1f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.startColor = new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, alpha);
            lineRenderer.endColor = new Color(lineRenderer.endColor.r, lineRenderer.endColor.g, lineRenderer.endColor.b, alpha);
            alpha -= 0.1f; // ������ ���������� ���ҽ��� ������� ȿ�� ����
        }
    }
}