using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour 
{
    private Inventory inventory;
    public Transform itemSlotContainer;
    public GameObject itemSlot;

    [Header("Item Preview")]
    [SerializeField] private Image img_item;
    [SerializeField] private TextMeshProUGUI txt_ItemName;
    public Button btn_equipItem;
    public Button btn_RemoveFromInventory;

    [SerializeField] private Sprite sprite_EmptyInventory;

    private Item lastHoveredItem;

    private void OnEnable()
    {
        if (inventory == null) return;

        inventory.ItemRemovedFromInventory += RefreshInventoryItems;
        DefaultPreview();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
        inventory.ItemRemovedFromInventory += RefreshInventoryItems;
        DefaultPreview();
    }

    public void RefreshInventoryItems() 
    {
        foreach(Transform child in  itemSlotContainer)                                                                                                                                   
        {
           if(child.TryGetComponent<ItemSlot>(out ItemSlot i))
            {
                i.OnHoverItem -= ItemGameObject_OnHoverItem;
                i.OnHoverExitItem -= ItemGameObject_OnHoverExitItem;
            }
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.GetItemsList())
        {
            ItemSlot itemGameObject = Instantiate(itemSlot, itemSlotContainer.transform).GetComponent<ItemSlot>();

            itemGameObject.OnHoverItem += ItemGameObject_OnHoverItem;
            itemGameObject.OnHoverExitItem += ItemGameObject_OnHoverExitItem;

            itemGameObject.gameObject.SetActive(true);
            itemGameObject.name = item.itemType.ToString();

            Image image = itemGameObject.transform.GetChild(0).GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI quantityText = itemGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            quantityText.text = item.quantity.ToString();

            itemGameObject.SetItem(item);
        }
    }

    private void ItemGameObject_OnHoverExitItem(Item obj)
    {
        SetLastHoveredItem(obj);
    }

    private void ItemGameObject_OnHoverItem(Item obj)
    {
        SetPreview(obj.GetSprite(), obj.GetItemName());
    }

    bool inventoryOpen = false;
    public void ToogleInventory()
    {
        if (!inventoryOpen)
        {
            inventoryOpen = true;
            gameObject.SetActive(true);
        }
        else
        {
            inventoryOpen = false;
            gameObject.SetActive(false);
        }

    }


    private void OnDisable()
    {
         inventory.ItemRemovedFromInventory -= RefreshInventoryItems;    
    }

    public void SetPreview(Sprite itemImage , string itemName)
    {
        this.txt_ItemName.text = itemName;
        this.img_item.sprite = itemImage;
    }

    public void DefaultPreview()
    {
        if(inventory.IsInventoryEmpty()) { SetPreview(sprite_EmptyInventory, "Inventory is Empty !"); ToogleInventoryButtons(false);  return; }

        Item i = inventory.GetFirstItemFromList();
        SetPreview(i.GetSprite(), i.GetItemName());
        ToogleInventoryButtons(true);
        lastHoveredItem = i;
    }

    private void ToogleInventoryButtons(bool status)
    {
        btn_equipItem.gameObject.SetActive(status);
        btn_RemoveFromInventory.gameObject.SetActive(status);
    }

    public void SetLastHoveredItem(Item lastHoveredItem)
    {
        this.lastHoveredItem = lastHoveredItem;
    }

    public Item GetLastHoveredItem()
    {
        return lastHoveredItem; 
    }


}
