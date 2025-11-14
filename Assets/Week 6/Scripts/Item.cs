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
        foreach (SO_Items item in ItemData.instance.items)
        {
            Debug.Log("item name : "  + item.name + "item type : " + item.itemType);
            Debug.Log("itemType : " + itemType);
            if (item.itemType == itemType)
            {
                return item.itemIcon;
            }
        }
        return null;
    }

    //is stackable
    public bool IsStackable()
    {
        foreach (SO_Items item in ItemData.instance.items)
        {
            if (item.itemType == itemType)
            {
                return item.isStackable;
            }
        }
        return false;
    }
}

public enum ItemType
{
    Weapon,
    HeaalthPotion,
    food,
    Shield,
    Arrow,
    ManaPotion,
    Bow
}

