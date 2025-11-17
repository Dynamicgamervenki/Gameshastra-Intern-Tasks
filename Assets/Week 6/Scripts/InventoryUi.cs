using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour 
{
    private Inventory inventory;

    public Transform itemSlotContainer;
    public GameObject itemSlot;

    private void OnEnable()
    {
        if(inventory != null)
            inventory.ItemRemovedFromInventory += RefreshInventoryItems;    
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
        inventory.ItemRemovedFromInventory += RefreshInventoryItems;
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
            itemGameObject.GetComponent<ItemSlot>().item = item;
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



}
