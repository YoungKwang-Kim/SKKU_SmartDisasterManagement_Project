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
    public float flightHeight = 10.0f; // 비행 높이
    public float readySpeed = 2.5f; // 이착륙 속도

    private int waypointIndex; // waypoint들의 인덱스

    bool isDroneStart = false;
    bool isRotate = true;

    public enum State { TakeOff, LeftFlight, CenterFlight, RightFlight, Return, Landing } //상태 설정
    public State droneState; //열거형을 담을 변수

    private void Start()
    {
        // 프로펠러 애니메이터 컴포넌트 가져오기.
        propAnim = myDrone.GetComponent<Animator>();
        droneLight.SetActive(false);

        waypointIndex = 0;
        // 드론의 처음 상태를 이륙상태로 만든다.
        droneState = State.TakeOff;
        // 시작버튼에 OnClickButton함수를 할당한다.
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

    // 드론의 상태에 따른 행동패턴.
    public void SwitchDroneState()
    {
        //드론 상태변화
        switch (droneState)
        {
            //이륙
            case State.TakeOff:
                if (leftWaypoints.Length == 0)
                {
                    Debug.Log("웨이포인트를 설정해주세요.");
                    break;
                }
                else if (leftWaypoints.Length > 0)
                {
                    droneLight.SetActive (true);
                    // 프로펠러 순서대로 회전시킨다.
                    StartPropeller();
                    // 이륙
                    myDrone.transform.Translate(Vector3.up * readySpeed * Time.deltaTime);
                    if (myDrone.transform.position.y > leftWaypoints[0].transform.position.y)
                    {
                        droneState = State.LeftFlight;
                    }
                }
                break;
            //왼쪽면 비행
            case State.LeftFlight:
                //두 거리를 비교한 뒤에 거리의 차이가 있다면 해당 waypoint로 이동
                if (Vector3.Distance(leftWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, leftWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //현재 waypoint에 도달한 상태라면(두 거리의 차가 0.1 이하라면)
                else
                {
                    waypointIndex++;
                    // waypoint를 다 돌았으면.
                    if (waypointIndex > leftWaypoints.Length - 1)
                    {
                        waypointIndex = 0;
                        isRotate = true;
                        droneState = State.CenterFlight;
                        
                    }
                }
                break;
            //가운데면 비행
            case State.CenterFlight:
                if (isRotate)
                {
                    StartCoroutine(RotateOverTime(myDrone, Vector3.up, 90f, 1.5f));
                    isRotate = false;
                }
                //두 거리를 비교한 뒤에 거리의 차이가 있다면 해당 waypoint로 이동
                if (Vector3.Distance(centerWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, centerWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //현재 waypoint에 도달한 상태라면(두 거리의 차가 0.1 이하라면)
                else
                {
                    waypointIndex++;
                    // waypoint를 다 돌았으면.
                    if (waypointIndex > centerWaypoints.Length - 1)
                    {
                        waypointIndex = 0;
                        isRotate = true;
                        droneState = State.RightFlight;
                    }
                }
                break;
            //오른쪽 면 비행
            case State.RightFlight:
                if (isRotate)
                {
                    StartCoroutine(RotateOverTime(myDrone, Vector3.up, 90f, 1.5f));
                    isRotate = false;
                }
                //두 거리를 비교한 뒤에 거리의 차이가 있다면 해당 waypoint로 이동
                if (Vector3.Distance(rightWaypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, rightWaypoints[waypointIndex].transform.position, flightSpeed);
                }
                //현재 waypoint에 도달한 상태라면(두 거리의 차가 0.1 이하라면)
                else
                {
                    waypointIndex++;
                    // waypoint를 다 돌았으면.
                    if (waypointIndex > rightWaypoints.Length - 1)
                    {
                        droneState = State.Return;
                    }
                }
                break;
            // 원래 자리로 복귀
            case State.Return:
                droneLight.SetActive(false);
                Vector3 returnPoint = new Vector3(waypointBase.position.x, myDrone.transform.position.y, waypointBase.transform.position.z);
                Move(myDrone, returnPoint, flightSpeed);

                if (Vector3.Distance(returnPoint, myDrone.transform.position) < 0.1f)
                {
                    droneState = State.Landing;
                }

                break;
            //착륙
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
    // 타겟포인트로 이동한다.
    void Move(GameObject gameobject, Vector3 targetPoint, float speed)
    {
        // gameObject가 있는 지점에서 타겟포인트까지의 방향을 구한다.
        Vector3 relativePosition = targetPoint - gameobject.transform.position;
        relativePosition.Normalize();

        // gameObject가 목표지점을 향해 날아간다.
        gameobject.transform.Translate(relativePosition * speed * Time.deltaTime, Space.World);
    }
    // 오브젝트를 duration동안 axis축을 기준으로 angle만큼 회전시키는 코루틴함수입니다.
    IEnumerator RotateOverTime(GameObject gameObject, Vector3 axis, float angle, float duration)
    {
        // 경과시간
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

    // 프로펠러를 회전시킨다.
    private void StartPropeller()
    {
        if (propAnim == null)
        {
            Debug.Log("프로펠러 애니메이션을 설정해주세요.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 1);
            }
        }
    }
    // 프로펠러를 멈춘다.
    private void PausePropeller()
    {
        if (propAnim == null)
        {
            Debug.Log("프로펠러 애니메이션을 설정해주세요.");
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
