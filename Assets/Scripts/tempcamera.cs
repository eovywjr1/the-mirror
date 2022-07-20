using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempcamera : MonoBehaviour
{
    Camera mirror;
    void Start()
    {
        mirror = GetComponent<Camera>();
        mirror.clearFlags = CameraClearFlags.Depth;
        mirror.depth = Camera.main.depth + 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
