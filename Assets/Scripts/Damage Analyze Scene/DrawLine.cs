using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // Line Renderer 프리팹
    public GameObject linePrefab;

    // Line Renderer 컴포넌트를 담을 변수
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
        // 두번째 이상 좌클릭
        else if (Input.GetMouseButtonDown(0) && lr.positionCount > 0)
        {
            points.Add(Input.mousePosition);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, new Vector3(points[lr.positionCount - 1].x, points[lr.positionCount - 1].y, -36));
        }
    }
}