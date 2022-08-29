using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractManageer : MonoBehaviour
{
    GameObject otherObject; //상호작용 가능한 상대 오브젝트, 없으면 null
    //bool interactable = false; //스크립트 달려있어도 상호작용 불가할경우 -> 추후 구현 예정

    // Start is called before the first frame update
    Type[] interactionTypes = new Type[] { typeof(ConversationInteractionEventReceiver), typeof(TeleportEventReceiver) }; //상호작용 가능한 모든 클래스 종류
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Interact(int id) //0 : interact 1, 1 : stopInteract
    {
        if (otherObject == null)
        {
            return;
        }

        InteractionEvent conversationInteractionEventReceiver;

        if (otherObject.gameObject.name.Equals("Bed"))
        {
            GameObject character = GameManager.player;
            conversationInteractionEventReceiver = character.GetComponent<ConversationInteractionEventReceiver>();
            character.GetComponent<DialogManager>().SetId(2);
        }

        //임시로 이렇게 하고 여유 있을 때 상속기능 활용해서 합칠 예정
        
        else
        {
            Component c = null;
            foreach (Type t in interactionTypes) //상호작용 가능한 모든 컴포넌트 탐색
            {
                c = otherObject.GetComponent(t);
                if (c != null)
                    break;

            }
            if (c == null) //상호작용 가능한 물체가 아님
            {
                
                return;
            }
            conversationInteractionEventReceiver = (InteractionEvent) c;


        }
        
        switch (id)
        {
            
            case 0:
                
                if(conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.Interact(transform.parent.gameObject);
                break;
            case 1:
                if (conversationInteractionEventReceiver != null)
                    conversationInteractionEventReceiver.StopInteract(transform.parent.gameObject);
                break;


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        otherObject = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Interact(1);
            otherObject = null;
    }
}
