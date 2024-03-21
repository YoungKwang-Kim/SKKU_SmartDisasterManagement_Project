using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // ���� �׸� LineRenderer ������Ʈ
    private LineRenderer lineRenderer;

    // ���� �������� ������ ������ ����
    private Vector3 startPoint;
    private Vector3 endPoint;

    void Start()
    {
        // LineRenderer ������Ʈ�� �߰��մϴ�.
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // ���� �β� ����
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // �� ���� ����
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // ���� ���� ���� (���⼭�� �������� ������ ������ ���̹Ƿ� 2�� ����)
        lineRenderer.positionCount = 2;

        // �ʱ�ȭ ���¿����� ���� ������ �ʵ��� ����
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ���̵��� ����
            lineRenderer.enabled = true;

            // �������� ���콺 Ŭ���� ��ġ�� ����
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0; // z ���� Canvas ��鿡 �����Ǿ�� �մϴ�.

            // ���� ������ ����
            lineRenderer.SetPosition(0, startPoint);
        }

        // ���콺 ���� ��ư�� �������� ��
        if (Input.GetMouseButtonUp(0))
        {
            // ������ ���콺 Ŭ���� ��ġ�� ����
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0; // z ���� Canvas ��鿡 �����Ǿ�� �մϴ�.

            // ���� ���� ����
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}