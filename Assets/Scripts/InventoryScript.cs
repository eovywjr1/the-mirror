using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> slotList, selectedList;
    public List<Item> itemList;
    public GameObject selected3, selected2, hp;
    public int moveIndex, selectedIndex, pageIndex = 0;
    public Sprite unmovedImage, movedImage, unselectedImage, selectedImage;
    public bool selectedItem;

    void Start()
    {
        slotList[0].GetComponent<Image>().sprite = movedImage;
        hp.GetComponent<Transform>().SetAsFirstSibling();
        ShowFirstPageItem();
    }

    void Update()
    {
        Moved();
        Selected();
    }

    void Moved()
    {
        if (Input.GetKeyDown("w"))
        {
            if (!selectedItem && moveIndex <= 3)
            {
                Debug.Log("페이지 선택");
            }
            else if (!selectedItem && moveIndex > 3)
            {
                MovedIndex(slotList, (moveIndex % 12), (moveIndex - 4) % 12, unmovedImage, movedImage);
                moveIndex -= 4;
                ChangePageItem();
            }
            else if (selectedItem && selectedIndex != 0)
            {
                selectedIndex--;
                MovedIndex(selectedList, selectedIndex + 1, selectedIndex, unselectedImage, selectedImage);
            }
        }
        else if (Input.GetKeyDown("a") && moveIndex > 0 && !selectedItem)
        {
            MovedIndex(slotList, moveIndex % 12, (moveIndex - 1) % 12, unmovedImage, movedImage);
            moveIndex--;
            ChangePageItem();
        }
        else if (Input.GetKeyDown("s"))
        {
            if (!selectedItem && itemList.Count > moveIndex + 4)
            {
                MovedIndex(slotList, moveIndex % 12, (moveIndex + 4) % 12, unmovedImage, movedImage);
                moveIndex += 4;
                ChangePageItem();
            }
            else if (selectedItem && selectedIndex != 2)
            {
                selectedIndex++;
                MovedIndex(selectedList, selectedIndex - 1, selectedIndex, unselectedImage, selectedImage);
            }
        }
        else if (Input.GetKeyDown("d") && itemList.Count > moveIndex + 1 && !selectedItem)
        {
            MovedIndex(slotList, moveIndex % 12, (moveIndex + 1) % 12, unmovedImage, movedImage);
            moveIndex++;
            ChangePageItem();
        }
    }

    void Selected()
    {
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
        selected3.transform.position = new Vector2(slotList[moveIndex].transform.position.x + 215, slotList[moveIndex].transform.position.y - 65);
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

    void ShowFirstPageItem()
    {
        for (int i = 0; i < 12 && i < itemList.Count; i++)
            slotList[i].transform.GetChild(0).GetComponent<InventorySlot>().item = itemList[i];
    }

    void ChangePageItem()
    {
        if(pageIndex != moveIndex / 12)
        {
            pageIndex = moveIndex / 12;

            int i, j;
            for (i = pageIndex * 12, j = 0; i < pageIndex * 12 + 12 && i < itemList.Count; i++, j++)
                slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = itemList[i];

            for (; j < 12; j++)
                slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = null;
        }
    }
}
