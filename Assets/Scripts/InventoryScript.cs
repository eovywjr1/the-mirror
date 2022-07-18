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
    public bool selectedItem, pageSelected;

    private void OnEnable()
    {
        ChagngeMovedImage(slotList, (moveIndex % 12), 0, unmovedImage, movedImage);
        moveIndex = 0;
        pageIndex = 0;
        PageItemShow();
    }

    private void Start()
    {
        hp.GetComponent<Transform>().SetAsFirstSibling();
    }

    private void Update()
    {
        if (!pageSelected)
        {
            Selected();
            if(!selectedItem)
                ItemMoved();
            else
                SelectMoved();
        }
        else
            PageMoved();
    }

    //아이템 칸 / select 칸 이동
    void ItemMoved()
    {
        if (Input.GetKeyDown("w"))
        {
            if (moveIndex <= 3)
            {
                pageSelected = true;
                slotList[moveIndex].GetComponent<Image>().sprite = unmovedImage;
                //페이지 이미지 변경
            }
            else
            {
                ChagngeMovedImage(slotList, (moveIndex % 12), (moveIndex - 4) % 12, unmovedImage, movedImage);
                moveIndex -= 4;
                ChangePageItem();
            }
        }
        else if (Input.GetKeyDown("a") && moveIndex > 0)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex - 1) % 12, unmovedImage, movedImage);
            moveIndex--;
            ChangePageItem();
        }
        else if (Input.GetKeyDown("s") && 24 > moveIndex + 4)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex + 4) % 12, unmovedImage, movedImage);
            moveIndex += 4;
            ChangePageItem();
        }
        else if (Input.GetKeyDown("d") && 24 > moveIndex + 1)
        {
            ChagngeMovedImage(slotList, moveIndex % 12, (moveIndex + 1) % 12, unmovedImage, movedImage);
            moveIndex++;
            ChangePageItem();
        }
    }

    //아이템 상호작용 창 이동
    void SelectMoved()
    {
        if (Input.GetKeyDown("w") && selectedIndex != 0)
        {
            selectedIndex--;
            ChagngeMovedImage(selectedList, selectedIndex + 1, selectedIndex, unselectedImage, selectedImage);
        }
        else if (Input.GetKeyDown("s") && selectedIndex != 2)
        {
            selectedIndex++;
            ChagngeMovedImage(selectedList, selectedIndex - 1, selectedIndex, unselectedImage, selectedImage);
        }
    }

    //페이지 이동
    void PageMoved()
    {
        if (Input.GetKeyDown("a") && pageIndex > 0)
        {
            //페이지 이미지 변경
            pageIndex--;
            PageItemShow();
        }
        else if (Input.GetKeyDown("d") && pageIndex == 0)
        {
            //페이지 이미지 변경
            pageIndex++;
            PageItemShow();
        }
        else if (Input.GetKeyDown("s"))
        {
            //페이지 이미지 변경
            pageSelected = false;
            moveIndex = pageIndex * 12;
            slotList[moveIndex % 12].GetComponent<Image>().sprite = movedImage;
        }
    }

    //인벤토리 상호작용
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

    //select 창 생성
    void ActiveSelected()
    {
        selected3.SetActive(true);
        selected3.transform.position = new Vector2(slotList[moveIndex].transform.position.x + 215, slotList[moveIndex].transform.position.y - 65);
        selectedItem = true;
        selectedList[0].GetComponent<Image>().sprite = selectedImage;
    }

    //select 창 제거
    void DeactiveSelected()
    {
        selectedList[selectedIndex].GetComponent<Image>().sprite = unselectedImage;
        selectedIndex = 0;
        selectedItem = false;
        selected3.SetActive(false);
    }

    //칸 이동할 때 이미지 변경
    void ChagngeMovedImage(List<GameObject> list, int preIndex, int nextIndex, Sprite unImage, Sprite doImage)
    {
        list[preIndex].GetComponent<Image>().sprite = unImage;
        list[nextIndex].GetComponent<Image>().sprite = doImage;
    }

    //페이지 변경 확인
    void ChangePageItem()
    {
        if(pageIndex != moveIndex / 12)
        {
            pageIndex = moveIndex / 12;
            PageItemShow();
        }
    }


    //페이지의 아이템 보여주기
    void PageItemShow()
    {
        int i, j;
        for (i = pageIndex * 12, j = 0; i < pageIndex * 12 + 12 && i < itemList.Count; i++, j++)
            slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = itemList[i];

        for (; j < 12; j++)
            slotList[j].transform.GetChild(0).GetComponent<InventorySlot>().item = null;
    }
}
