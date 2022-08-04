using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepManager : SelecteMoveScript
{
    public GameObject dayImage_prefab;
    GameObject today;

    public List<Sprite> dayImageList;

    void Update()
    {
        Move(1);
        Select();
    }

    public override void Select()
    {
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

    void YesSleep()
    {
        today = Instantiate(dayImage_prefab);

        FindObjectOfType<PlayerControllerScript>().gameObject.GetComponent<DialogManager>().DestroyBubble();
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        StartCoroutine(Fadeout());
    }

    void NoSleep()
    {
        Destroy(this.gameObject);
    }

    void AgainButTutorial()
    {
        //튜토리얼 씬 조건 추가예정
        DialogManager characterDialogManager = FindObjectOfType<PlayerControllerScript>().gameObject.GetComponent<DialogManager>();
        characterDialogManager.bedSettutorialindex = true;
        Destroy(this.gameObject);
    }

    IEnumerator Fadeout()
    {
        Image image = today.transform.GetChild(0).GetComponent<Image>();
        Color color = image.color;

        while (color.a < 1)
        {
            color.a += 0.1f;
            image.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        image.sprite = dayImageList[++FindObjectOfType<GameManager>().day - 2];
        image.color = new Color(1, 1, 1, 1);

        StartCoroutine(DeleteDayImage());
    }

    IEnumerator DeleteDayImage()
    {
        yield return new WaitForSecondsRealtime(1f);

        Destroy(today);
        Destroy(this.gameObject);
    }
}
