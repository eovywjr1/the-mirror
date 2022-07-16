using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventoryScript>().gameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
            inventory.SetActive(!inventory.activeSelf);
    }
}
