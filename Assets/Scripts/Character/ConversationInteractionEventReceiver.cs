using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConversationInteraction : InteractionEvent
{
    [SerializeField]
    DialogManager dialogManager;
    public ConversationInteraction(DialogManager originDialogManager, GameObject obj)
    {
        dialogManager = originDialogManager;
    }
    public void Approach(GameObject you, GameObject me)
    {

    }

    public void Interact(GameObject you, GameObject me)
    {

        dialogManager.StartConversation();
    }

    public void StopInteract()
    {
        dialogManager.EndConversation();
    }
}
public class ConversationInteractionEventReceiver : MonoBehaviour //상호작용 받는 역할
{
    
    ConversationInteraction conversationInteractionClass;
    public void Awake()
    {
        conversationInteractionClass = new ConversationInteraction(GetComponent<DialogManager>(), gameObject) ;
        conversationInteractionClass.Interact(gameObject, gameObject);
    }
        
}