using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenEffectManager : MonoBehaviour
{
    [SerializeField]
    CameraScreenEffect[] effects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateEffect(int id)
    {
        effects[id].Activate();
    }
}
