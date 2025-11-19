using System;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Item item;

    #region events
    public event Action<Item> OnHoverItem;
    public event Action<Item> OnHoverExitItem;
    #endregion
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverItem.Invoke(this.item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         OnHoverExitItem?.Invoke(this.item);    
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
