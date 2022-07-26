using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    
    [SerializeField]
    float playerMaxSpeed;               // 플레이어가 최대로 내는 속도
    [SerializeField]
    float shiftMultiplyRate;            //쉬프트 누르면 몇배로 빨라지는가?
    [SerializeField]
    float playerAcceleration;           // 민첩도? 플레이어 이동 가속도
    int playerState = 0;                // 애니메이션에 넣기 위한 플레이어 움직임 상태
    Vector3 speed;                      // 플레이어 현재속도
    float directionX = 0;                  // 가로 움직이는 방향
    float directionY = 0;               //세로 움직이는 방향
    [SerializeField]
    RuntimeAnimatorController[] playerAnimationontrollerList; //상화좌우 애니메이션
    [SerializeField]
    float breakThreshold;               // 이 속도 이하면 아예 멈춤
    [SerializeField]
    float animationBreakThreshold;      //에니매이션에 해당
    Animator playerAnimationController; //걸음걸이 애니메이션 컨트롤러
    ScreenCoordinateCorrector corrector; //좌표 보정용
    GameObject interactor; //상호작용 범위 콜라이더
    InteractManageer interactManageer;//상호작용 스크립트
    float previousSpeedX = 0.0f;//이전 가로 속도
    float previousSpeedY = 0.0f;//이전 가로 속도
    bool isShiftPressed = false; //만약 달리는 도중 특정 행동을 할 수 없을 때에 대비
    float spriteWidthInUnit;
    float spriteHeightInUnit;



    // Start is called before the first frame update
    private void Awake()
    {
        corrector = new ScreenCoordinateCorrector();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidthInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.x); 
        spriteHeightInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.y);
        interactor = transform.GetChild(0).gameObject;
        interactManageer = interactor.GetComponent<InteractManageer>();
    }
    void Start()
    {
        speed = new Vector3(0, 0, 0);
        playerAnimationController = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInteractEvent();
        isShiftPressed = Input.GetKey(KeyCode.LeftShift);
    }

    private void ProcessInteractEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactManageer.Interact(0);
        }
    }

    private void FixedUpdate()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        Move(axisHorizontal, axisVertical);
        UpdateAnimationParameter();
        //rotation
        /*
        if(axisHorizontal * axisVertical == 0 || axisHorizontal * previousSpeedX <= 0 || axisHorizontal * previousSpeedX <= 0)
        {
            Debug.Log(axisHorizontal);
            if(axisHorizontal * previousSpeedX <= 0 && axisHorizontal != 0 || (axisVertical == 0 && axisHorizontal != 0))
            {

                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            }
            if(axisHorizontal * previousSpeedX <= 0 && axisVertical != 0 || (axisHorizontal == 0 && axisVertical != 0))
            {
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));
            }
            //모두가 0일때는 그냥 플레이어 회전 그대로
        }
        */
        
            float absAxisHorizontal = Mathf.Abs(axisHorizontal);
            float absAxisVertical = Mathf.Abs(axisVertical);
            if (absAxisHorizontal > absAxisVertical && axisHorizontal != 0)
            {
                Debug.Log("Updated2");
                RotateHorizontal(Convert.ToInt32(axisHorizontal / Mathf.Abs(axisHorizontal)));
            }
            if (absAxisVertical > absAxisHorizontal && axisVertical != 0)
            {
                Debug.Log("Updated");
                RotateVertical(Convert.ToInt32(axisVertical / Mathf.Abs(axisVertical)));

            }
        

        previousSpeedX = axisHorizontal;
        previousSpeedY = axisVertical;

    }
    //입력 받아서 플레이어 움직이는 함수 (속도 좌표에 더하는 방식으로 할것)
    private void Move(float x, float y)
    {

        directionX = x;
        directionY = y;

        //새로운 속도 계산
        Vector3 newSpeed = new Vector3(directionX,directionY,0);

        //속도 계산한것 가지고 state 결정, 아직 속도가 변화 안함
        UpdateState(speed.x , newSpeed.x,speed.y, newSpeed.y); 

        //캐릭터 속도 반영
        transform.position += newSpeed * playerMaxSpeed * Time.deltaTime;
        if (isShiftPressed)
            transform.position += newSpeed * playerMaxSpeed * Time.deltaTime * (Convert.ToInt16(isShiftPressed) * shiftMultiplyRate);

        /*
        if (Mathf.Abs(newSpeed) >= breakThreshold) 
            gameObject.transform.position += (speed * Time.deltaTime);
        */

            //기존 속도 업데이트
        speed = newSpeed;
        
    }
    void RotateVertical(int direction)//플레이어 움직이는 방향에 따라 회전해줄 함수
    {
        //캐릭터 회전
        int index = (int)(0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        //콜라이더 회전
        interactor.transform.localPosition = new Vector3(0, (spriteHeightInUnit / 2  + interactor.transform.localScale.y)*direction , 0);
       
    }
    void RotateHorizontal(int direction)
    {
        int index = (int)(2 + 0.5 * direction + 0.5f);
        playerAnimationController.runtimeAnimatorController = playerAnimationontrollerList[index];
        interactor.transform.localPosition = new Vector3((spriteWidthInUnit / 2   + interactor.transform.localScale.x )*direction, 0, 0);
    }

    //속도 계산해서 달리는지, 멈추는 중인지, 멈추는지 상태 표시
    void UpdateState(float previousSpeedX, float newSpeedX, float previousSpeedY, float newSpeedY)
    {
        float deltaSpeedX = Mathf.Abs(newSpeedX) - Mathf.Abs(previousSpeedX); //속도 크기 차이
        float deltaSpeedY = Mathf.Abs(newSpeedY) - Mathf.Abs(previousSpeedX);
        //if (speed.x <= animationBreakThreshold && speed.x >= -animationBreakThreshold)
        if(newSpeedX == 0 && newSpeedY == 0)
        {
            playerState = 0;
        }
        else if (newSpeedX > 0.0f && newSpeedX < 1.0f
              && newSpeedY > 0.0f && newSpeedY < 1.0f
            && (deltaSpeedX < 0 || deltaSpeedY < 0))
        {
            if( deltaSpeedY * deltaSpeedX == 0)
                playerState = 2;
            
        }
       else //deltaSpeed > 0
        {
            playerState = 1;
            //방향에 따라 캐릭터 좌우 반전
            //if (Input.GetAxis("Horizontal") != 0)
            //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, speed.x < 0 ? 180f : 0, 0));

        }

    }
    //전역변수에 있는 state를 애니메이션 컨트롤러 파라미터로 전달하는 함수
    void UpdateAnimationParameter()
    {
        playerAnimationController.SetInteger("state", playerState);
    }
}
