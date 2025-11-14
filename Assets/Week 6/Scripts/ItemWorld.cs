using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public ItemType itemType;
    public int amount;  

    public Item GetItem()
    {
       return new Item(itemType, amount);
    }

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
