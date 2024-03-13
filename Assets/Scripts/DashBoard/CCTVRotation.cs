using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVRotation : MonoBehaviour
{
    public Transform cam1_y;
    public Transform cam2_y;
    public Transform cam1_x;
    public Transform cam2_x;

    Vector3 firstPos;
    bool isEnable = false;
    float rotationSpeed = 10f;
    float minAngle = -45f;
    float maxAngle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = cam1_x.position;
    }

    private void Update()
    {

    }

    public void CamRotation()
    {
        if (isEnable)
        {
            // ȸ���� ������ ���
            float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            // ���� ȸ�� ������ ������
            float currentAngle = cam1_x.localEulerAngles.y;

            // ���� ����
            float newAngle = Mathf.Clamp(currentAngle + rotationAmount, minAngle, maxAngle);

            // ȸ����Ŵ
            cam1_x.localEulerAngles = new Vector3(cam1_x.localEulerAngles.x, newAngle, cam1_x.localEulerAngles.z);
        }
    }
}
