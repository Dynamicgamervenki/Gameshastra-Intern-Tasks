using System;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Item item;
    private Cube player;
    private InventoryUi inventoryUi;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Cube>();
        inventoryUi = player.GetInventoryUi();
    }

    private Item lastHoveredItem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryUi.SetPreview(item.GetSprite(),item.GetItemName());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        lastHoveredItem = this.item;
        player.SetLastHoveredItem(lastHoveredItem);
      //  Debug.LogWarning("lastHoveredItem : " + lastHoveredItem.GetItemName());
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
