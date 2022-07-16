using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> itemList, selectedList;
    public GameObject selected3, selected2, hp;
    public int moveIndex, selectedIndex;
    public Sprite unmovedImage, movedImage, unselectedImage, selectedImage;
    public bool selectedItem;

    void Start()
    {
        itemList[moveIndex].GetComponent<Image>().sprite = movedImage;
        hp.GetComponent<Transform>().SetAsFirstSibling();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w")) {
            if (!selectedItem && moveIndex > 3) {
            
                moveIndex -= 4;
                MovedIndex(itemList, moveIndex + 4, moveIndex, unmovedImage, movedImage);
            }
            else if(selectedItem && selectedIndex != 0)
            {
                selectedIndex--;
                MovedIndex(selectedList, selectedIndex + 1, selectedIndex, unselectedImage, selectedImage);
            }
        }
        if (Input.GetKeyDown("a") && moveIndex > 0 && !selectedItem)
        {
            moveIndex--;
            MovedIndex(itemList, moveIndex + 1, moveIndex, unmovedImage, movedImage);
        }
        if (Input.GetKeyDown("s")) {
            if (!selectedItem && itemList.Count > moveIndex + 4){
                moveIndex += 4;
                MovedIndex(itemList, moveIndex - 4, moveIndex, unmovedImage, movedImage);
            }
            else if (selectedItem && selectedIndex != 2)
            {
                selectedIndex++;
                MovedIndex(selectedList, selectedIndex - 1, selectedIndex, unselectedImage, selectedImage);
            }
        }
        if (Input.GetKeyDown("d") && itemList.Count > moveIndex + 1 && !selectedItem)
        {
            moveIndex++;
            MovedIndex(itemList, moveIndex - 1, moveIndex, unmovedImage, movedImage);
        }
        if (Input.GetKeyDown("e"))
        {
            if (!selectedItem)
                ActiveSelected();
            else if (selectedIndex == 2)
                DeactiveSelected();
        }
    }

    void ActiveSelected()
    {
        selected3.SetActive(true);
        selected3.transform.position = new Vector2(itemList[moveIndex].transform.position.x + 215, itemList[moveIndex].transform.position.y - 65);
        selectedItem = true;
        selectedList[0].GetComponent<Image>().sprite = selectedImage;
    }

    void DeactiveSelected()
    {
        selectedList[selectedIndex].GetComponent<Image>().sprite = unselectedImage;
        selectedIndex = 0;
        selectedItem = false;
        selected3.SetActive(false);
    }

    void MovedIndex(List<GameObject> list, int preIndex, int nextIndex, Sprite unImage, Sprite doImage)
    {
        list[preIndex].GetComponent<Image>().sprite = unImage;
        list[nextIndex].GetComponent<Image>().sprite = doImage;
    }
}
