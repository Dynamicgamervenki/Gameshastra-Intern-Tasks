using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour 
{

    [Header("Player Data")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    public PlayerState currentPlayerState;
    [SerializeField] private Transform weaponSocket;

    [Header("Script References")]
    [SerializeField] private GameInput gameInput;
    public InventoryUi inventoryUi;

    [Header("Level Data")]
    [SerializeField] private int leveltoLoad = 0;

    #region privateVariables
    private Inventory inventory;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isMoving = false;
    private ItemWorld EquippedItem;
    private List<ItemWorld> SpawnedWeapons = new List<ItemWorld>();
    #endregion

    public bool IsMoving()
    {
       return isMoving;
    }

    private void Start()
    {
        currentPlayerState = PlayerState.Unarmed;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnInventoryAction += GameInput_OnInventoryAction;
        rb = GetComponent<Rigidbody>();

        inventory = new Inventory();

        inventoryUi.SetInventory(inventory);
        inventoryUi.btn_equipItem.onClick.AddListener(EquipItem);
        inventoryUi.btn_RemoveFromInventory.onClick.AddListener(RemoveTheItemFromInventoryAndSpawnInfronOfPlayer);
    }

    private void GameInput_OnInventoryAction(object sender, EventArgs e)
    {
        inventoryUi.ToogleInventory();
    }


    public void AddItemToInventory(Item item)
    {
        inventory.AddToList(item);
        inventoryUi.RefreshInventoryItems();
        inventoryUi.DefaultPreview();
        
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        if ((isGrounded))
        {
            Jump();
        }
    }


    private void Update()
    {
        MovementUsingNewInputSystem();      
    }

    private void FixedUpdate()
    {

        if(newMove != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(newMove);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

    }
    Vector3 newMove = Vector3.zero;

    private void MovementUsingNewInputSystem()
    { 
        newMove = gameInput.GetMovementVectorNormalized();
        rb.MovePosition(transform.position + newMove * moveSpeed * Time.deltaTime);
        isMoving = newMove != Vector3.zero;

    }


    private void Jump()
    {
        if(rb)
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0));
    }

    public void TestingDead()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
       // Material material = GetComponent<Renderer>().material;

        //if(material)
        //    material.color = Color.red;

        if(UiManager.Instance)
            UiManager.Instance.GameOver();

        if (Camera.main.TryGetComponent<CameraMovementt>(out var camera))
            camera.ShakeCamera();

        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(leveltoLoad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(Dead());
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Quaternion reset = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.localRotation, reset, 0.6f);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<ItemWorld>(out ItemWorld itemWorld))
        {
              AddItemToInventory(itemWorld.GetItem());

            if(inventory.IsInventoryFull && !itemWorld.GetItem().IsStackable())
            {
                return;
            }
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameInput.OnJumpAction -= GameInput_OnJumpAction;
        gameInput.OnInventoryAction -= GameInput_OnInventoryAction;
    }



    public void EquipItem()
    {
        Item i = inventoryUi.GetLastHoveredItem();
        if (!i.CanAttachToSocket()) return;

        if (currentPlayerState == PlayerState.armed)
        {
            Unequip();
        }
        CheckIfWeaponIsAlreadySpawned(i);
        EquippedItem.gameObject.SetActive(true);
        currentPlayerState = PlayerState.armed;
    }

    private void CheckIfWeaponIsAlreadySpawned(Item item)
    {

        if(SpawnedWeapons.Count != 0)
        {

            foreach (ItemWorld ii in SpawnedWeapons)
            {
                if (ii.itemType == item.itemType)
                {
                    EquippedItem = ii;
                    return;
                }
            }
        }

        EquippedItem = Instantiate(item.GetItemPrefab(), weaponSocket.position, weaponSocket.rotation, weaponSocket);
        SpawnedWeapons.Add(EquippedItem);
    }

    private void RemoveTheItemFromInventoryAndSpawnInfronOfPlayer()
    {
        Item it = inventoryUi.GetLastHoveredItem();
        if (currentPlayerState == PlayerState.armed && it.itemType == GetEquiipedItem().itemType)
        {
            Unequip();
        }

        Vector3 spawnPos = (transform.position + transform.forward * 5);
        spawnPos.y = 0.75f;

        foreach (SO_Items i in ItemData.Instance.items)
        {
            if (i.itemType == it.itemType)
            {
                int quantity = inventory.GetItemQuantity(it);
                for (int j = 0; j < quantity; j++)
                {
                    Instantiate(i.item, spawnPos, Quaternion.identity);
                    spawnPos.x += 2.0f;
                }
                inventory.RemoveItemFromInventoryList(it);
                inventoryUi.DefaultPreview();
                if (inventory.IsInventoryEmpty) { it = null; return; }
                it = inventory.GetItemsList()[0];
                return;
            }
        }
    }

    public void Unequip()
    {
        EquippedItem.gameObject.SetActive(false);   
        currentPlayerState = PlayerState.Unarmed;
    }

    public ItemWorld GetEquiipedItem()
    {
        return EquippedItem;
    }

}

public enum PlayerState
{
    Unarmed,
    armed
}