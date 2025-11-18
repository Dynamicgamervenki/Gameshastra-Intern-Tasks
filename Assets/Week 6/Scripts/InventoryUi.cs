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
            Destroy(child.gameObject);
        }
        foreach (Item item in inventory.GetItemsList())
        {
            GameObject itemGameObject = Instantiate(itemSlot, itemSlotContainer.transform);
            itemGameObject.SetActive(true);
            itemGameObject.name = item.itemType.ToString();
            Image image = itemGameObject.transform.GetChild(0).GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI quantityText = itemGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            quantityText.text = item.quantity.ToString();
            itemGameObject.GetComponent<ItemSlot>().SetItem(item);
        }
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
        Item i = inventory.GetItemsList()[0];
        SetPreview(i.GetSprite(), i.GetItemName());
        ToogleInventoryButtons(true);
    }

    private void ToogleInventoryButtons(bool status)
    {
        btn_equipItem.gameObject.SetActive(status);
        btn_RemoveFromInventory.gameObject.SetActive(status);
    }


}
