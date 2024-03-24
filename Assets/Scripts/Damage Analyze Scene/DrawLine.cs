using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    // Line Renderer ������
    public GameObject linePrefab;
    // DamagedCell�� �����Ǵ� ��ġ
    public RectTransform DamageChartPanel;
    // ������ ��
    public SaveDamageCell DamageCell;
    // ����ȣ
    private int cellNum = 0;
    // ��ư�� ���ȴ��� ����
    private bool isSketchButtonOn;
    // Line Renderer�� ����
    private List<Color> colorList = new List<Color>{ Color.red, Color.green, Color.blue, Color.cyan, Color.yellow  };
    private int colorNum = 0;

    // Line Renderer ������Ʈ�� ���� ����
    LineRenderer lr;
    // Ŭ������ ���� ���콺�� ������ ���� �����Ѵ�.
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
        // ù��° ��Ŭ��
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
        // �ι�° �̻� ��Ŭ��
        else if (Input.GetMouseButtonDown(0) && lr.positionCount > 0)
        {
            points.Add(Input.mousePosition);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, new Vector3(points[lr.positionCount - 1].x, points[lr.positionCount - 1].y, -36));
        }
        // ���콺 ��Ŭ������ ��
        else if (Input.GetMouseButtonDown(1))
        {
            // �� �Ÿ��� ���ϱ�
            float total = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                float distance = Vector3.Distance(points[i], points[i + 1]);
                total += distance;
            }
            
            DamageCell.SetDamageCell(cellNum, (points.Count < 3) ? "Straight" : "Thunder", total, DamageChartPanel);
            cellNum++;
            points.Clear();
            // �� ���� ��ȯ
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