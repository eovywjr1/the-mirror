using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerPassingEvent : DialogEvent
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            eventCallDelegate.Invoke(dialogID);
        }
        
    }
    
}
