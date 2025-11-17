using System;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    private Cube player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Cube>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            HandleRightClick();
        else if (eventData.button == PointerEventData.InputButton.Left)
            HandleLeftClick();
    }

    private void HandleRightClick()
    {
        RemoveTheItemFromInventoryAndSpawnInfronOfPlayer();
    }

    private void HandleLeftClick()
    {
        if (player.currentPlayerState == PlayerState.armed )
        {
            player.Unequip();
        }
        ItemWorld invenotryItem = item.GetItemPrefab();
        player.EquipItem(invenotryItem);
    }

    private void RemoveTheItemFromInventoryAndSpawnInfronOfPlayer()
    {
        if (player.currentPlayerState == PlayerState.armed &&  item.itemType == player.GetEquiipedItem().itemType )
        {
            player.Unequip();
        }

        Inventory inventory = player.GetInventory();
        Vector3 spawnPos = (player.transform.position + player.transform.forward * 5);
        spawnPos.y = 0.75f;

        foreach (SO_Items i in ItemData.instance.items)
        {
            if(i.itemType == item.itemType)
            {
                int quantity = inventory.GetItemQuantity(item);
                for(int j=0;j<quantity;j++)
                {
                    Instantiate(i.item,spawnPos,Quaternion.identity);
                    spawnPos.x += 2.0f;
                }
                inventory.RemoveItemFromInventoryList(item);
                return;
            }
        }
    }



}
