using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions inputActions;

    #region Inputevents
    public event EventHandler OnJumpAction;
    public event EventHandler OnInventoryAction;
    public event EventHandler OnEquipAction;
    #endregion 

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Inventory.performed += Inventory_performed;
        inputActions.Player.Equip.performed += Equip_performed;
    }

    private void Equip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEquipAction?.Invoke(this,EventArgs.Empty);
    }

    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInventoryAction?.Invoke(this,EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 inputVector_ = new Vector3(inputVector.x, 0f, inputVector.y);

        //inputVector = inputVector.normalized;
        inputVector_ = inputVector_.normalized;

        //return inputVector;
        return inputVector_;
    }


}

