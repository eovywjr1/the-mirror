using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownEventTest : DialogEvent
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDownCoroutine()
    {
        yield return new WaitForSeconds(3f);
        eventCallDelegate.Invoke(index);
    }
}
