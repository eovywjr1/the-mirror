using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogEvent;

public class DialogEventHandler : MonoBehaviour
{
    //반드시 dialogmanager와 함께 붙어야 함
    EventCallDelegate eventCallDelegate;
    [SerializeField]

    DialogEvent[] dialogEvents;
    DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GetComponent<DialogManager>(); //value does not fall in ???
        eventCallDelegate = dialogManager.CallDialogByEvent;
        for(int i=0; i<dialogEvents.Length; i++)
        {

            dialogEvents[i].InitEvent(i, eventCallDelegate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
