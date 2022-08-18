using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeleportEventReceiver : InteractionEvent
    
{
    string locationCSVFile = "Assets\\location.csv";
    public override void Approach(GameObject player)
    {
        throw new System.NotImplementedException();
    }

    public override void Interact(GameObject player)
    {
        

        StreamReader reader = new StreamReader(locationCSVFile);
        for (int i = 0; i < 1; i++)
            reader.ReadLine();
        string line = reader.ReadLine(); //id�� 1������ ����
        int x = Convert.ToInt32(line.Split(',')[0]);
        int y = Convert.ToInt32(line.Split(',')[1]);
        GameObject.Find("Player").transform.position = new Vector2(x, y);
    }

    public override void StopInteract(GameObject player)
    {
        throw new System.NotImplementedException();
    }

}