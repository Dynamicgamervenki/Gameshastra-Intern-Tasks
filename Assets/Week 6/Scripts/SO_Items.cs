using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "InventoryItems/SO_Items")]
public class SO_Items : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public bool isStackable;
}
