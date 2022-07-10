using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractionEvent
{
    void Interact(GameObject you, GameObject me);
    void Approach(GameObject you, GameObject me);
    void StopInteract();
    
}
