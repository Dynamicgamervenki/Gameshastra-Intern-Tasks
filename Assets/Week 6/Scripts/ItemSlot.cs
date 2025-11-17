using System;
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
        SpawnInFrontOfPlayer();
    }

    private void SpawnInFrontOfPlayer()
    {
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
