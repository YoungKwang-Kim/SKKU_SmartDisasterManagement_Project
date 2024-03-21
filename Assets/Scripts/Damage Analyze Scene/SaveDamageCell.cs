using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveDamageCell : MonoBehaviour
{
    // DamageCell의 속성들
    public TextMeshProUGUI _number;
    public TextMeshProUGUI _damaged;
    public TextMeshProUGUI _value;
    // Cell 사이의 공간
    public float space = 30f;

    // DamageCell의 값을 넣어주는 메서드
    public void SetDamageCell(int number, string damaged, float value, RectTransform parent)
    {
        _number.text = number.ToString();
        _damaged.text = damaged;
        _value.text = value.ToString("F1") + "m";

        GameObject newCell = Instantiate(gameObject, parent);
        RectTransform newCellTransform = newCell.GetComponent<RectTransform>();
        // Anchor를 패널의 상단에 두었기 때문에 마이너스가 붙는다.
        float yPosition = -(newCellTransform.sizeDelta.y + space) * (number - 1);
        // 계산한 yPosition값에 패널의 높이값을 더해준다. 그래야지 위부터 차곡차곡 생성됩니다.
        newCellTransform.localPosition = new Vector3(0, parent.sizeDelta.y + yPosition, 0);
    }
}