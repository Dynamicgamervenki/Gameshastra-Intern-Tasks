using UnityEngine;

public class Item
{
    public ItemType itemType;
    public int quantity;

    public Item() { }
    public Item(ItemType itemType , int quantity) 
    {
        this.itemType = itemType;
        this.quantity = quantity;
    }

    public Sprite GetSprite()
    {
        foreach (SO_Items item in ItemData.Instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.itemIcon;
            }
        }
        return null;
    }


    public bool IsStackable()
    {
        foreach (SO_Items item in ItemData.Instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.isStackable;
            }
        }
        return false;
    }

    public ItemWorld GetItemPrefab()
    {
        foreach (SO_Items item in ItemData.Instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.item;
            }
        }
        return null;
    }

    public bool CanAttachToSocket()
    {
        foreach (SO_Items item in ItemData.Instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.canAttackToSocket;
            }

        }
        return false;
    }

    public string GetItemName()
    {
        foreach (SO_Items item in ItemData.Instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.itemName;
            }
        }
        return default;
    }
}

public enum ItemType
{
    Weapon,                 //this is sword acutally , didnt want to change here and again fix references in inspector
    HeaalthPotion,
    food,
    Shield,
    Arrow,
    ManaPotion,
    Bow
}

