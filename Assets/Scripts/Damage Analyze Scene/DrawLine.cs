using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    // Line Renderer 프리팹
    public GameObject linePrefab;
    // DamagedCell이 생성되는 위치
    public RectTransform DamageChartPanel;
    // 데미지 셀
    public SaveDamageCell DamageCell;
    // 셀번호
    private int cellNum = 0;
    // 버튼이 눌렸는지 여부
    private bool isSketchButtonOn;
    // Line Renderer의 색깔
    private List<Color> colorList = new List<Color>{ Color.red, Color.green, Color.blue, Color.cyan, Color.yellow  };
    private int colorNum = 0;

    // Line Renderer 컴포넌트를 담을 변수
    LineRenderer lr;
    // 클릭했을 때의 마우스의 포지션 값을 저장한다.
    List<Vector3> points = new List<Vector3>();

    private void Start()
    {
        isSketchButtonOn = false;
        cellNum = 1;
    }

    private void Update()
    {
        if (isSketchButtonOn)
        {
            DrawLineAndGetDistance();
        }
    }

    public void OnClickButton()
    {
        isSketchButtonOn = !isSketchButtonOn;
        
    }

    public void DrawLineAndGetDistance()
    {
        // 첫번째 좌클릭
        if (Input.GetMouseButtonDown(0) && points.Count == 0)
        {
            GameObject go = Instantiate(linePrefab, gameObject.transform);
            lr = go.GetComponent<LineRenderer>();
            lr.startColor = colorList[colorNum];
            lr.endColor = colorList[colorNum];
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
        // 마우스 우클릭했을 때
        else if (Input.GetMouseButtonDown(1))
        {
            // 총 거리값 구하기
            float total = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                float distance = Vector3.Distance(points[i], points[i + 1]);
                total += distance;
            }
            
            DamageCell.SetDamageCell(cellNum, (points.Count < 3) ? "Straight" : "Thunder", total, DamageChartPanel);
            cellNum++;
            points.Clear();
            // 선 색깔 전환
            if (colorNum < colorList.Count - 1)
            {
                colorNum++;
            }
            else
            {
                colorNum = 0;
            }
            Debug.Log(total);
        }
    }
}