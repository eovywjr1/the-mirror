using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeleportScript : InteractionEvent
{
    string locationCSVFile = "Assets\\location.csv";
    [SerializeField]
    int teleportID;
    public override void Approach(GameObject player)
    {

    }

    public override void Interact(GameObject player)
    {
        StreamReader reader = new StreamReader(locationCSVFile);
        for (int i = 0; i < teleportID; i++)
            reader.ReadLine();
        string line = reader.ReadLine(); //id는 1번부터 시작
        int x = Convert.ToInt32(line.Split(',')[0]);
        int y = Convert.ToInt32(line.Split(',')[0]);
        player.transform.position = new Vector2(x, y);
    }

    public override void StopInteract(GameObject player)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
