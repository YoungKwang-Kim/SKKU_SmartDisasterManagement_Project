using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    public GameObject myDrone;
    public GameObject droneLight;
    private Animator propAnim;
    public Transform[] leftWaypoints, rightWaypoints, centerWaypoints;
    public Transform waypointBase;
    public float flightSpeed;
    public Button startButton;
    public float flightHeight = 10.0f; // ���� ����
    public float readySpeed = 2.5f; // ������ �ӵ�

    private int waypointIndex; // waypoint���� �ε���

    bool isDroneStart = false;
    bool isRotate = true;

    public enum State { TakeOff, LeftFlight, CenterFlight, RightFlight, Return, Landing } //���� ����
    public State droneState; //�������� ���� ����

    private void Start()
    {
        // �����緯 �ִϸ����� ������Ʈ ��������.
        propAnim = myDrone.GetComponent<Animator>();
        droneLight.SetActive(false);

        waypointIndex = 0;
        // ����� ó�� ���¸� �̷����·� �����.
        droneState = State.TakeOff;
        // ���۹�ư�� OnClickButton�Լ��� �Ҵ��Ѵ�.
        //startButton.onClick.AddListener(OnClickButton);

    }

    private void Update()
    {
        if (isDroneStart)
        {
            SwitchDroneState();
        }
    }

    public void OnClickDroneStart()
    {
        isDroneStart = true;
    }

    // ����� ���¿� ���� �ൿ����.
    public void SwitchDroneState()
    {
        //��� ���º�ȭ
        switch (droneState)
        {
            //�̷�
            case State.TakeOff:
                if (leftWaypoints.Length == 0)
                {
                    Debug.Log("��������Ʈ�� �������ּ���.");
                    break;
                }
                else if (leftWaypoints.Length > 0)
                {
                    droneLight.SetActive (true);
                    // �����緯 ������� ȸ����Ų��.
                    StartPropeller();
                    // �̷�
                    myDrone.transform.Translate(Vector3.up * readySpeed * Time.deltaTime);
                    if (myDrone.transform.position.y > leftWaypoints[0].transform.position.y)
                    {
                        droneState = State.LeftFlight;
                    }
                }
                break;
            //���ʸ� ����
            case State.LeftFlight:
                //�� �Ÿ��� ���� �ڿ� �Ÿ��� ���̰� �ִٸ� �ش� waypoint�� �̵�
                if (Vector3.Distance(leftWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, leftWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //���� waypoint�� ������ ���¶��(�� �Ÿ��� ���� 0.1 ���϶��)
                else
                {
                    waypointIndex++;
                    // waypoint�� �� ��������.
                    if (waypointIndex > leftWaypoints.Length - 1)
                    {
                        waypointIndex = 0;
                        isRotate = true;
                        droneState = State.CenterFlight;
                        
                    }
                }
                break;
            //����� ����
            case State.CenterFlight:
                if (isRotate)
                {
                    StartCoroutine(RotateOverTime(myDrone, Vector3.up, 90f, 1.5f));
                    isRotate = false;
                }
                //�� �Ÿ��� ���� �ڿ� �Ÿ��� ���̰� �ִٸ� �ش� waypoint�� �̵�
                if (Vector3.Distance(centerWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, centerWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //���� waypoint�� ������ ���¶��(�� �Ÿ��� ���� 0.1 ���϶��)
                else
                {
                    waypointIndex++;
                    // waypoint�� �� ��������.
                    if (waypointIndex > centerWaypoints.Length - 1)
                    {
                        waypointIndex = 0;
                        isRotate = true;
                        droneState = State.RightFlight;
                    }
                }
                break;
            //������ �� ����
            case State.RightFlight:
                if (isRotate)
                {
                    StartCoroutine(RotateOverTime(myDrone, Vector3.up, 90f, 1.5f));
                    isRotate = false;
                }
                //�� �Ÿ��� ���� �ڿ� �Ÿ��� ���̰� �ִٸ� �ش� waypoint�� �̵�
                if (Vector3.Distance(rightWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, rightWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //���� waypoint�� ������ ���¶��(�� �Ÿ��� ���� 0.1 ���϶��)
                else
                {
                    waypointIndex++;
                    // waypoint�� �� ��������.
                    if (waypointIndex > rightWaypoints.Length - 1)
                    {
                        droneState = State.Return;
                    }
                }
                break;
            // ���� �ڸ��� ����
            case State.Return:
                droneLight.SetActive(false);
                Vector3 returnPoint = new Vector3(waypointBase.position.x, myDrone.transform.position.y, waypointBase.transform.position.z);
                Move(myDrone, returnPoint, flightSpeed);

                if (Vector3.Distance(returnPoint, myDrone.transform.position) < 0.1f)
                {
                    droneState = State.Landing;
                }

                break;
            //����
            case State.Landing:

                myDrone.transform.Translate(Vector3.down * readySpeed * Time.deltaTime);
                if (myDrone.transform.position.y < waypointBase.transform.position.y)
                {
                    readySpeed = 0;
                    PausePropeller();
                }
                break;
        }

    }
    // Ÿ������Ʈ�� �̵��Ѵ�.
    void Move(GameObject gameobject, Vector3 targetPoint, float speed)
    {
        // gameObject�� �ִ� �������� Ÿ������Ʈ������ ������ ���Ѵ�.
        Vector3 relativePosition = targetPoint - gameobject.transform.position;
        relativePosition.Normalize();

        // gameObject�� ��ǥ������ ���� ���ư���.
        gameobject.transform.Translate(relativePosition * speed * Time.deltaTime, Space.World);
    }
    // ������Ʈ�� duration���� axis���� �������� angle��ŭ ȸ����Ű�� �ڷ�ƾ�Լ��Դϴ�.
    IEnumerator RotateOverTime(GameObject gameObject, Vector3 axis, float angle, float duration)
    {
        // ����ð�
        float elaspedTime = 0f;
        Quaternion startRotation = gameObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(axis * angle) * startRotation;

        while (elaspedTime < duration)
        {
            float t = Mathf.Clamp01(elaspedTime / duration);
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.rotation = targetRotation;
    }

    // �����緯�� ȸ����Ų��.
    private void StartPropeller()
    {
        if (propAnim == null)
        {
            Debug.Log("�����緯 �ִϸ��̼��� �������ּ���.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 1);
            }
        }
    }
    // �����緯�� �����.
    private void PausePropeller()
    {
        if (propAnim == null)
        {
            Debug.Log("�����緯 �ִϸ��̼��� �������ּ���.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 0);
            }
        }
    }

}
