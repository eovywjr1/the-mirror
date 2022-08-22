using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    public delegate void EventCallDelegate(int id);
    public int index;
    public EventCallDelegate eventCallDelegate;
    public int dialogID;
    // Start is called before the first frame update
    public void InitEvent(int idx, EventCallDelegate d)
    {
        index = idx;
        eventCallDelegate = d;
    }
    private void Start()
    {
        //event body
    }
    private void Awake()
    {
        //event body
    }
    

}