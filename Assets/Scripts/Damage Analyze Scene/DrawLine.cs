using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // Line Renderer 프리팹
    public GameObject linePrefab;

    LineRenderer lr;
    // 클릭했을 때의 마우스의 포지션 값을 저장한다.
    List<Vector3> points = new List<Vector3>();


    private void Update()
    {
        // 첫번째 좌클릭
        if (Input.GetMouseButtonDown(0) && points.Count == 0)
        {
            GameObject go = Instantiate(linePrefab, gameObject.transform);
            lr = go.GetComponent<LineRenderer>();
            points.Add(Input.mousePosition);
            lr.positionCount = 1;
            lr.SetPosition(0, new Vector3(points[0].x, points[0].y, -36));
        }
        // 두번째 좌클릭
        else if (Input.GetMouseButtonDown(0) && points.Count == 1)
        {
            points.Add(Input.mousePosition);
            lr.positionCount = 2;
            lr.SetPosition(1, new Vector3(points[1].x, points[1].y, -36));
        }
    }
}