using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item _item;
    public Item item
    {
        get { return _item; }
        set { _item = value;
            Image image = this.GetComponent<Image>();

            if (_item != null)
            {
                image.color = new Color(1, 1, 1, 1);
                image.sprite = _item.itemImage;
            }
            else
                image.color = new Color(1, 1, 1, 0);
        }
    }
}
