using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInputAxis : MonoBehaviour //단순히 키보드 입력을 플레이어에게 전달
{

    PlayerControllerScript playerControllerScript; //플레이어에 붙어있는 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerControllerScript.SetHorizontalAxis(Input.GetAxis("Horizontal"));
        playerControllerScript.SetVerticalAxis(Input.GetAxis("Vertical"));
        playerControllerScript.SetShiftPressed(Input.GetKey(KeyCode.LeftShift));
    }
}
