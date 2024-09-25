using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 움직임 속도를 설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;          //이동속도
    public float jumpForce = 5.0f;          //점프 힘
    //카메라 설정 변수
    [Header("Camerra Settings")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 2.0f;   //마우스 감도

    public float radius = 5.0f;//3인칭 카메라와 플레이어 간의 거리
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;                            //카메라 수직 회전 최소각
    public float yMaxLimit = 90;                            //카메라 수직 회전 최대각

    private float theta = 0.0f;       //카메라의 수평 회전 각도
    private float phi = 0.0f;          //카메라의 수직 회전 각도
    private float tarrgetVericalRotaion = 0;     //목표 수직 회전 각도
    private float verticalRoatationSpeed = 240f; //수직 회전 각도

    //내부 변수들 
    private bool isFirstperson = true;       //1인칭 모드 인지 여부
    private bool isGrounded;                //플레이어가 땅에 있는지 여부
    private Rigidbody rb;                   //플레이어의 Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();     //RigidBody 컴포넌트를 가져온다.

        Cursor.lockState = CursorLockMode.Locked;     //마우스 커서를 잠그고 숨긴다.
        SetupCameras();
        SetActiveCamera();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        HandleCameraToggle();
    }

    //활성화할 카메라를 설정하는 함수
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstperson);
        thirdPersonCamera.gameObject.SetActive(!isFirstperson);
    }
    //카메라 초기 위치 밒 회전을 설정하는 함수
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);     //1인칭 카메라 위치
        firstPersonCamera.transform.localRotation = Quaternion.identity;               //1인칭 카메라 회전 초기화
    }
    //카메라 및 캐릭터 회전 처리하는 함수
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;              //마우스 좌우 입력
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;              //마우스 상하 입력

        //수평 회전 (theta 값)
        theta += mouseX;    //마우스 입력값 추가
        theta = Mathf.Repeat(theta, 360.0f);    //각도 값이 360를 넘지 않도록 조정 (0 ~ 360 | 361 -> 1)

        //수직 회전 처리
        tarrgetVericalRotaion -= mouseY;
        tarrgetVericalRotaion = Mathf.Clamp(tarrgetVericalRotaion, yMinLimit, yMaxLimit);  //수직 회전 제한
        phi = Mathf.MoveTowards(phi, tarrgetVericalRotaion, verticalRoatationSpeed * Time.deltaTime);

        //플레이어 회전(캐릭터가 수평으로만 회전)
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);
        if (isFirstperson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f); //1인칭 카메라 수직 회전
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float Y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float Z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, Y, Z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("Muse ScrollWheel") * 5, minRadius, maxRadius);
        }     
    }

    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstperson = !isFirstperson;    //카메라 모드 전환
            SetActiveCamera();
        }
    }
    //플레이어 점프를 처리하는 함수
    void HandleJump()
    {
        //점프 버튼을 누르고 땅에 있을때 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);     //위쪽으로 힘을 가해 점프
            isGrounded = false;
        }
    }

    //플레이이의 이동을 처리하는 함수 
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");             //좌우 입력 (-1 ~ 1)
        float moveVertical = Input.GetAxis("Vertical");                 //앞뒤 입력 (1 ~ -1)

        if (!isFirstperson)//3인칭 모드일때, 카메라 방향으로 이동처리
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward; 
            cameraForward.y = 0.0f;
            cameraForward.Normalize();  //방향 벡터 정규화 (0~1) 사이 값으로 만들어준다

            Vector3 cameraRight = thirdPersonCamera.transform.right;    //카메라 오른쪽 방향
            cameraRight.y = 0.0f;
            cameraRight.Normalize();
        }
        else
        {
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);  // 물리 기반 이동
        }

          
    }

    //플레이어가 땅에 닿아 있는지 감지
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;              //충돌 중이면 플레이어는 땅에 있다.
    }

}