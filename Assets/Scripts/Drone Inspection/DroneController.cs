using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    public GameObject myDrone;
    private Animator propAnim;
    public Transform[] waypoints;
    public Transform waypointBase;
    public float flightSpeed;
    public Button startButton;
    public float flightHeight = 10.0f; // ���� ����
    public float readySpeed = 2.5f; // ������ �ӵ�

    private int waypointIndex; // waypoint���� �ε���

    bool isDroneStart = false;

    public enum State { TakeOff, Flight, Return, Landing } //���� ����
    public State droneState; //�������� ���� ����

    private void Start()
    {
        // �����緯 �ִϸ����� ������Ʈ ��������.
        propAnim = myDrone.GetComponent<Animator>();

        waypointIndex = 0;
        // ����� ó�� ���¸� �̷����·� �����.
        droneState = State.TakeOff;
        // ���۹�ư�� OnClickButton�Լ��� �Ҵ��Ѵ�.
        //startButton.onClick.AddListener(OnClickButton);

    }

    private void Update()
    {
        //if (isDroneStart)
        //{
        //    SwitchDroneState();
        //}
        SwitchDroneState();
    }

    public void OnClickButton()
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
                if (waypoints.Length == 0)
                {
                    Debug.Log("��������Ʈ�� �������ּ���.");
                    break;
                }
                else if (waypoints.Length > 0)
                {
                    // �����緯 ������� ȸ����Ų��.
                    StartPropeller();
                    // �̷�
                    myDrone.transform.Translate(Vector3.up * readySpeed * Time.deltaTime);
                    if (myDrone.transform.position.y > waypoints[0].transform.position.y)
                    {
                        droneState = State.Flight;
                    }
                }
                break;
            //����
            case State.Flight:
                //�� �Ÿ��� ���� �ڿ� �Ÿ��� ���̰� �ִٸ� �ش� waypoint�� �̵�
                if (Vector3.Distance(waypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, waypoints[waypointIndex].transform.position, flightSpeed);
                }
                //���� waypoint�� ������ ���¶��(�� �Ÿ��� ���� 0.1 ���϶��)
                else
                {
                    waypointIndex++;
                    // waypoint�� �� ��������.
                    if (waypointIndex > waypoints.Length - 1)
                    {
                        droneState = State.Return;
                    }
                }
                break;
            // ���� �ڸ��� ����
            case State.Return:

                Move(myDrone, waypointBase.transform.position, flightSpeed);

                if (Vector3.Distance(waypointBase.transform.position, myDrone.transform.position) < 0.1f)
                {
                    droneState = State.Landing;
                }

                break;
            //����
            case State.Landing:

                myDrone.transform.Translate(Vector3.down * readySpeed * Time.deltaTime);
                if (myDrone.transform.position.y < 0.5)
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
        

        // �� ���� ������ �������� normal, �� ���� ���̸� RotateTowards�� ������. �� �����ӿ� 1����
        // gameobject.transform.rotation = Quaternion.RotateTowards(gameobject.transform.rotation, Quaternion.LookRotation(relativePosition), 2f);
        // gameObject�� ��ǥ������ ���� ���ư���.
        gameobject.transform.Translate(relativePosition * speed * Time.deltaTime, Space.World);
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
