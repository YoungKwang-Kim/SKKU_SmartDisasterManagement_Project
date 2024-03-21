using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // Line Renderer ������
    public GameObject linePrefab;

    // Line Renderer ������Ʈ�� ���� ����
    LineRenderer lr;
    // Ŭ������ ���� ���콺�� ������ ���� �����Ѵ�.
    List<Vector3> points = new List<Vector3>();

    private void Update()
    {
        // ù��° ��Ŭ��
        if (Input.GetMouseButtonDown(0) && points.Count == 0)
        {
            GameObject go = Instantiate(linePrefab, gameObject.transform);
            lr = go.GetComponent<LineRenderer>();
            points.Add(Input.mousePosition);
            lr.positionCount = 1;
            lr.SetPosition(0, new Vector3(points[0].x, points[0].y, -36));
        }
        // �ι�° �̻� ��Ŭ��
        else if (Input.GetMouseButtonDown(0) && lr.positionCount > 0)
        {
            points.Add(Input.mousePosition);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, new Vector3(points[lr.positionCount - 1].x, points[lr.positionCount - 1].y, -36));
        }
    }
}