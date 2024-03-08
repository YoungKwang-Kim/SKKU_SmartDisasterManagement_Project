using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SimulationTest : MonoBehaviour
{
    public GameObject beforeCellFraction;
    public GameObject afterCellFraction;

    private void Start()
    {

    }

    public void OnButtonClick()
    {
        beforeCellFraction.SetActive(false);
        Instantiate(afterCellFraction, new Vector3(beforeCellFraction.transform.position.x, 0, beforeCellFraction.transform.position.z), Quaternion.identity);
    }

    public void ResetButton()
    {
        // "AfterCellFractionTest" 이름을 가진 모든 게임 오브젝트를 찾아서 제거
        GameObject[] afterCellFractions = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in afterCellFractions)
        {
            if (obj.name == "AfterCellFractionTest(Clone)")
            {
                Destroy(obj);
            }
        }
        beforeCellFraction.SetActive(true);
    }
}