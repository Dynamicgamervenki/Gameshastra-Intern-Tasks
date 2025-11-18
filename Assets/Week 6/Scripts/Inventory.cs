using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    private List<Item> items; 
    private int maxSlots = 24;

    public event Action ItemRemovedFromInventory;


    public Inventory()
    {
         items = new List<Item>();
        AddToList(new Item { itemType = ItemType.HeaalthPotion,quantity = 1});
        AddToList(new Item { itemType = ItemType.Weapon,quantity = 1});
       // Debug.LogWarning("Inventory");
       // Testing();
    }



    void Testing()
    {
        for(int i=0;i<=22; i++)
        {
            AddToList(new Item { itemType = ItemType.Shield, quantity = 1 });
        }
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
                Add(item);  // Item does not exist, add it
            }
        }
        else
        {
            Add(item);
        }
    }

    private void Add(Item item)
    {
        if (isInventoryFull()) return;

        items.Add(item);
    }

    public List<Item> GetItemsList() {
        return items;
    }

    public int GetItemQuantity(Item item)
    {
        foreach(Item i in items)
        {
            if(i.itemType == item.itemType)
                return i.quantity;
        }
        return 0;
    }

    public void RemoveItemFromInventoryList(Item item)
    {
        foreach (Item i in items)
        {
            if (i.itemType == item.itemType)
            {
                items.Remove(i);
                ItemRemovedFromInventory?.Invoke();
                return;
            }
        }
    }

    public bool isInventoryFull() { return items.Count >= maxSlots;}

    public bool IsInventoryEmpty() { return items.Count == 0; }

    public Item GetLastItemFromList() { return items[items.Count - 1]; }

    public Item GetFirstItemFromList() { return items[0]; }

}
