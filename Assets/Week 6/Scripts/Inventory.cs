using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items; 
    private int maxSlots = 24;

    public Inventory()
    {
         items = new List<Item>();
        AddToList(new Item { itemType = ItemType.HeaalthPotion,quantity = 1});
        AddToList(new Item { itemType = ItemType.Weapon,quantity = 1});
        AddToList(new Item { itemType = ItemType.food,quantity = 1});
        Debug.LogWarning("Inventory");
    }

    public void AddToList(Item item)
    {
        CheckIfItemExists(item);
      //  items.Add(item);
    }

    public void CheckIfItemExists(Item item)
    {
        if(item.IsStackable())
        {
            bool itemExists = false;
            foreach (Item i in items)
            {
                if (i.itemType == item.itemType)
                {
                    i.quantity += item.quantity;
                    itemExists = true;
                    return;     // Item already exists, no need to add again
                }
            }
            if (!itemExists)
            {
                items.Add(item);  // Item does not exist, add it
            }
        }
        else
        {
            items.Add(item);
        }
    }



    public List<Item> GetItemsList() {
        return items;
    }

    public int MaxSlots() { 
        return maxSlots;
    }


}
