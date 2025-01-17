using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    public int day;

    public static GameObject player;
    public static PlayerControllerScript playerControllerScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = player.GetComponent<PlayerControllerScript>();

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (inventory != null && Input.GetKeyDown("i"))
            inventory.SetActive(!inventory.activeSelf);
    }
}
