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
            // 회전할 각도를 계산
            float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            // 현재 회전 각도를 가져옴
            float currentAngle = cam1_x.localEulerAngles.y;

            // 각도 제한
            float newAngle = Mathf.Clamp(currentAngle + rotationAmount, minAngle, maxAngle);

            // 회전시킴
            cam1_x.localEulerAngles = new Vector3(cam1_x.localEulerAngles.x, newAngle, cam1_x.localEulerAngles.z);
        }
    }
}
