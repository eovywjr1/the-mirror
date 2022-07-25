using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepManager : MonoBehaviour
{
    [SerializeField]
    int index;
    public List<Image> panelList;

    void Update()
    {
        if(Input.GetKeyDown("w") && index != 0)
            ChangePanel(index, --index);
        if(Input.GetKeyDown("s") && index != 1)
            ChangePanel(index, ++index);
        if (Input.GetKeyDown("e"))
        {
            if (index == 0)
                YesSleep();
            else
                NoSleep();
        }
    }

    void ChangePanel(int preIndex, int index)
    {
        panelList[index].color = new Color(1, 1, 1);
        panelList[preIndex].color = new Color(0.23f, 0.23f, 0.7f);
    }

    void YesSleep()
    {
        //Day++? Day7?
    }

    void NoSleep()
    {
        FindObjectOfType<PlayerControllerScript>().gameObject.GetComponent<DialogManager>().SetId(6);
        Destroy(this.gameObject);
    }
}
