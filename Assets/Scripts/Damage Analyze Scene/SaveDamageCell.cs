using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveDamageCell : MonoBehaviour
{
    // DamageCell�� �Ӽ���
    public TextMeshProUGUI _number;
    public TextMeshProUGUI _damaged;
    public TextMeshProUGUI _value;
    // Cell ������ ����
    public float space = 30f;

    // DamageCell�� ���� �־��ִ� �޼���
    public void SetDamageCell(int number, string damaged, float value, RectTransform parent)
    {
        _number.text = number.ToString();
        _damaged.text = damaged;
        _value.text = value.ToString("F1") + "m";

        GameObject newCell = Instantiate(gameObject, parent);
        RectTransform newCellTransform = newCell.GetComponent<RectTransform>();
        // Anchor�� �г��� ��ܿ� �ξ��� ������ ���̳ʽ��� �ٴ´�.
        float yPosition = -(newCellTransform.sizeDelta.y + space) * (number - 1);
        // ����� yPosition���� �г��� ���̰��� �����ش�. �׷����� ������ �������� �����˴ϴ�.
        newCellTransform.localPosition = new Vector3(0, parent.sizeDelta.y + yPosition, 0);
    }
}