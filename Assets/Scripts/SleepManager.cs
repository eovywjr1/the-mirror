using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepManager : MonoBehaviour
{
    [SerializeField]
    int index;
    public List<Image> panelList;

    private void Start()
    {
        index = -1;
    }

    void Update()
    {
        if (Input.GetKeyDown("w")) {
            if (index > 0)
                ChangePanel(index, --index);
            else
            {
                index = 1;
                ChangePanel(index, index);
            }

        }
        if (Input.GetKeyDown("s")) {
            if (index >= 0)
                ChangePanel(index, ++index);
            else
            {
                index = 0;
                ChangePanel(index, index);
            }
        }
        if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))
        {
            if (index == -1)
            {
                AgainButTutorial();
            }
            else
            {
                if (index == 0)
                    YesSleep();

                else
                    NoSleep();
            }        
        }
    }

    void ChangePanel(int preIndex, int index)
    {
        panelList[preIndex].color = new Color(0.23f, 0.23f, 0.7f);
        panelList[index].color = new Color(1, 1, 1);
    }

    void YesSleep()
    {
        //Day++? Day7?
    }

    void NoSleep()
    {
        Destroy(this.gameObject);
    }

    void AgainButTutorial()
    {
        //튜토리얼 씬 조건 추가예정
        DialogManager characterDialogManager = FindObjectOfType<PlayerControllerScript>().gameObject.GetComponent<DialogManager>();
        characterDialogManager.SetIndex(5);
        Destroy(this.gameObject);
    }
}
