using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public ItemType itemType;
    public int amount;  

    public Item GetItem()
    {
       return new Item(itemType, amount);
    }

  
}
