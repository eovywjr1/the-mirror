using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    float playerMaxSpeed;               // 플레이어가 최대로 내는 속도
    [SerializeField]
    float playerAcceleration;           // 민첩도? 플레이어 이동 가속도
    int playerState = 0;                // 애니메이션에 넣기 위한 플레이어 움직임 상태
    Vector3 speed;                      // 플레이어 현재속도
    int direction = 0;                  // 움직이는 방향
    [SerializeField]
    float breakThreshold;               // 이 속도 이하면 아예 멈춤
    [SerializeField]
    float animationBreakThreshold;      //에니매이션에 해당
    Animator playerAnimationController; //걸음걸이 애니메이션 컨트롤러



    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector3(0, 0, 0);
        playerAnimationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateAnimationParameter();
    }
    //입력 받아서 플레이어 움직이는 함수 (꼬꼬마때처럼 속도 좌표에 더하는 방식으로 할것)
    private void Move()
    {
        //오른쪽 이동
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction += 1;
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            direction -= 1;
        }

        //왼쪽 이동
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction -= 1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            direction += 1;
        }
        //바로 이전 속도 값 저장
        float previousSpeed = speed.x * Time.deltaTime;
        
        //속도 서서히 변화시켜 주는 기능
        float beforeSpeed = 0; //최댓값 최솟값 고려 안한 상태의 속도 값
        if (direction == 0)
        {
            beforeSpeed = speed.x +  ((speed.x < 0 ? 1 : -1) * playerAcceleration) * Time.deltaTime; //속도가 0일때 고려해서 반대 방향으로 가속, 현재 플레이어가 이동중인 속도의 반대 방향으로 가속
            
            if (beforeSpeed > 0)
                speed.x = Mathf.Clamp(beforeSpeed, 0, playerMaxSpeed); //원래 오른쪽으로 이동중이었으면 왼쪽 방향으로는 못가게
            else
                speed.x = Mathf.Clamp(beforeSpeed, -playerMaxSpeed, 0); //원래 왼쪽으로 이동중이었으면 오른쪽으로 이동 못하게
        }
        else
        {
            beforeSpeed = speed.x +  (direction * playerAcceleration) * Time.deltaTime; //이동 방향 그대로 가속
            speed.x = Mathf.Clamp(beforeSpeed, -playerMaxSpeed, playerMaxSpeed);
        }
        float newSpeed = (speed.x * Time.deltaTime); //멈췄을 때 미세한 떨림 방지 용도
        UpdateState(previousSpeed , newSpeed); //속도 계산한것 가지고 state 결정, 아직 속도가 변화 안함
        if (Mathf.Abs(newSpeed) >= breakThreshold) 
            gameObject.transform.position += (speed * Time.deltaTime);
        
    }

    //속도 계산해서 달리는지, 멈추는 중인지, 멈추는지 상태 표시
    void UpdateState(float previousSpeed, float newSpeed)
    {
        float deltaSpeed = Mathf.Abs(newSpeed) - Mathf.Abs(previousSpeed); //속도 크기 차이
        if (speed.x <= animationBreakThreshold && speed.x >= -animationBreakThreshold)
        {
            playerState = 0;
        }
        else if (deltaSpeed < 0)
        {
            playerState = 2;
            
        }
       else //deltaSpeed > 0
        {
            playerState = 1;
            
        }

    }
    //전역변수에 있는 state를 애니메이션 컨트롤러 파라미터로 전달하는 함수
    void UpdateAnimationParameter()
    {
        playerAnimationController.SetInteger("state", playerState);
    }
}
