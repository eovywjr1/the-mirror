using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> itemList, selectedList;
    public GameObject selected;
    public int moveIndex, selectedIndex;

    void Start()
    {
        itemList[moveIndex].GetComponent<Image>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w")) {
            if (!selected.activeSelf && moveIndex > 3) {
            
                moveIndex -= 4;
                MovedIndex(itemList, moveIndex + 4, moveIndex);
            }
            else if(selected.activeSelf && selectedIndex != 0)
            {
                selectedIndex--;
                MovedIndex(selectedList, selectedIndex + 1, selectedIndex);
            }
        }
        if (Input.GetKeyDown("a") && moveIndex % 4 > 0! && !selected.activeSelf)
        {
            moveIndex--;
            MovedIndex(itemList, moveIndex + 1, moveIndex);
        }
        if (Input.GetKeyDown("s")) {
            if (!selected.activeSelf && itemList.Count > moveIndex + 4){
                moveIndex += 4;
                MovedIndex(itemList, moveIndex - 4, moveIndex);
            }
            else if (selected.activeSelf && selectedIndex != 2)
            {
                selectedIndex++;
                MovedIndex(selectedList, selectedIndex - 1, selectedIndex);
            }
        }
        if (Input.GetKeyDown("d") && moveIndex % 4 < 3 && itemList.Count > moveIndex + 1 && !selected.activeSelf)
        {
            moveIndex++;
            MovedIndex(itemList, moveIndex - 1, moveIndex);
        }
        if (Input.GetKeyDown("e"))
        {
            if (!selected.activeSelf)
                ActiveSelected();
            else if (selectedIndex == 2)
                DeactiveSelected();
        }
    }

    void ActiveSelected()
    {
        selected.SetActive(true);
        selected.transform.position = new Vector2(itemList[moveIndex].transform.position.x + 215, itemList[moveIndex].transform.position.y - 65);
        selectedList[0].GetComponent<Image>().color = Color.white;
    }

    void DeactiveSelected()
    {
        selectedList[selectedIndex].GetComponent<Image>().color = Color.black;
        selectedIndex = 0;
        selected.SetActive(false);
    }

    void MovedIndex(List<GameObject> list, int preIndex, int nextIndex)
    {
        list[preIndex].GetComponent<Image>().color = Color.black;
        list[nextIndex].GetComponent<Image>().color = Color.white;
    }
}
