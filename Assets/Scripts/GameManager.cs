using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    public int day;

    void Update()
    {
        if (inventory != null && Input.GetKeyDown("i"))
            inventory.SetActive(!inventory.activeSelf);
    }
}
