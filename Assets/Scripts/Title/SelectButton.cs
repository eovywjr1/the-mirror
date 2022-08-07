using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : SelecteMoveScript
{
    // Update is called once per frame
    void Update()
    {
        Move(2);
        Select();
    }
    public override void Move(int maxIndex)
    {
        if (Input.GetKeyDown("w"))
        {
            if (index == -1)
            {
                index = 2;
                ChangePanel(index, index);
            }
            else if (index > 0)
                ChangePanel(index, --index);
            else
            {
                index = 2;
                ChangePanel(0, index);
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (index == -1)
            {
                index = 0;
                ChangePanel(index, index);
            }
            else if (index < maxIndex)
                ChangePanel(index, ++index);
            else
            {
                index = 0;
                ChangePanel(2, index);
            }
        }
    }

    public override void Select()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                Debug.Log("게임 실행");
            }
            else if (index == 1)
            {
                Debug.Log("게임 로드");
            }
            else
            {
                Debug.Log("게임 종료");
                Application.Quit();
            }
        }
    }
}
