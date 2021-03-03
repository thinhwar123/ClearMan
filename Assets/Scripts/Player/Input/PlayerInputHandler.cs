using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
   [SerializeField] private PlayerInput playerInput;
    #region GamePlayActionInput
    public Vector2 rawMovementInput { get; private set; }
    public int normalizeInputX { get; private set; }
    public int normalizeInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool dashInput { get; private set; }
    public bool changeSoulwardInput { get; private set; }
    public bool castAbilityInput { get; private set; }
    public bool stopCastAbilityInput { get; private set; }
    public bool moveObjectInput { get; private set; }
    public bool healInput { get; private set; }
    public bool openBookInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool saveInput { get; private set; }
    #endregion


    #region UIActionInput
    public int changePageInput { get; private set; }
    public bool triggerButtonInput { get; private set; }
    public int changeButtonInputX { get; private set; }
    public int changeButtonInputY { get; private set; }
    public bool cancelInput { get; private set; }
    #endregion

    [SerializeField]
    private float inputHoldTime =0.2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private bool canCast;
    public void Start()
    {
        canCast = true;
    }
    public void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        normalizeInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        normalizeInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            jumpInputStop = true;
        }

    }
    public void UseJumpInput()
    { 
        jumpInput = false;
    }
    public void UseInteractInput()
    {
        normalizeInputY = 0;
    }
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
            dashInputStartTime = Time.time;
        }
    }
    public void UseDashInput()
    {
        dashInput = false;
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            dashInput = false;
        }
    }
    public void OnChangeSoulwardInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            changeSoulwardInput = true;
        }
        if (context.canceled)
        {
            changeSoulwardInput = false;
        }
    }
    public void OnCastSoulwardAbilityInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canCast)
            {
                castAbilityInput = true;
            }
        }
        if (context.canceled)
        {
            castAbilityInput = false;
        }
    }
    public void UseCastAbilityInput()
    {
        castAbilityInput = false;
    }
    public void OnStopCastSoulwardAbilityInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stopCastAbilityInput = true;
   
        }
        if (context.canceled)
        {
            stopCastAbilityInput = false;
        }
    }
    public void UseStopCastAbilityInput()
    {
        stopCastAbilityInput = false;
    }
    public void OnPushAndPullInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveObjectInput = true;
        }
        if (context.canceled)
        {
            moveObjectInput = false;
        }
    }
    public void UseMoveObjectInput()
    {
        moveObjectInput = false;
    }
    public void OnHealInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            healInput = true;
        }
        if (context.canceled)
        {
            healInput = false;
        }
    }
    public void UseHealInput()
    {
        healInput = false;
    }
    public void OnOpenBookInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            openBookInput = true;
        }
        if (context.canceled)
        {
            openBookInput = false;
        }
    }
    public void UseOpenBookInput()
    {
        openBookInput = false;
    }
    public void SetOpenBookInput(bool openBookInput)
    {
        this.openBookInput = openBookInput;
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput = true;
        }
        if (context.canceled)
        {
            attackInput = false;
        }
    }
    public void UseAttackInput()
    {
        attackInput = false;
    }
    public void OnSaveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            saveInput = true;
        }
        if (context.canceled)
        {
            saveInput = false;
        }
    }
    public void UseSaveInput()
    {
        saveInput = false;
    }
    public void DelayCastInput(float time)
    {
        StartCoroutine(DelayCastInputIEnumerator(time));
    }
    IEnumerator DelayCastInputIEnumerator(float time)
    {
        canCast = false;
        yield return new WaitForSecondsRealtime(time);
        canCast = true;
    }
    public void ChangeActionMap(string actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap);
    }
    public void OnChangePageInput(InputAction.CallbackContext context)
    {
        changePageInput = (int) context.ReadValue<float>();
    }
    public void OnChangeButtonInput(InputAction.CallbackContext context)
    {
        Vector2 vector2Input = context.ReadValue<Vector2>();
        changeButtonInputX = (int)(vector2Input * Vector2.right).normalized.x;
        changeButtonInputY = (int)(vector2Input * Vector2.up).normalized.y;
    }
    
    public void UseChangeButtonInputX()
    {
        changeButtonInputX = 0;
    }
    public void UseChangeButtonInputY()
    {
        changeButtonInputY = 0;
    }
    public void OnTriggerButtonInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            triggerButtonInput = true;
        }
        else if (context.canceled)
        {
            triggerButtonInput = false;
        }
    }
    public void UseTriggerButtonInput()
    {
        triggerButtonInput = false;
    }
    public void OnCancelInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            cancelInput = true;
            Debug.Log("ESC");
        }
        else if (context.canceled)
        {
            cancelInput = false;
        }
    }
    public void UseCancelInput()
    {
        cancelInput = false;
    }
}
