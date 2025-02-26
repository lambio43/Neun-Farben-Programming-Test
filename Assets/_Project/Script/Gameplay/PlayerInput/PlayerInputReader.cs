using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using static InputSystem_Actions;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObject/Player/InputReader")]
public class PlayerInputReader : SerializedScriptableObject, IPlayerActions, IUIActions
{
    public Subject<Vector2> Movement;
    public Subject<Vector2> Look;
    public Subject<Unit> Attack;
    public Subject<Unit> Jump;
    public Subject<Unit> Dash;
    public Subject<Unit> Reset;
    
    private InputSystem_Actions _inputActions;
    
    private void OnEnable()
    {
        if (_inputActions != null)
            return;
        
        Initialize();
    }
    
    private void Initialize()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
        _inputActions.UI.SetCallbacks(this);

        Cursor.lockState = CursorLockMode.Locked;

        Movement = new Subject<Vector2>();
        Attack = new Subject<Unit>();
        Jump = new Subject<Unit>();
        Dash = new Subject<Unit>();
        Look = new Subject<Vector2>();
        Reset = new Subject<Unit>();
    }
    
    public void EnablePlayerActions() 
    {
        _inputActions.Enable();
        _inputActions.Player.Enable();
        _inputActions.UI.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement.OnNext(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.OnNext(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) 
        {
            Attack.OnNext(UniRx.Unit.Default);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) 
        {
            Reset.OnNext(UniRx.Unit.Default);
            Debug.Log("working");
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       if (context.phase == InputActionPhase.Started) 
        {
            Jump.OnNext(UniRx.Unit.Default);
        } 
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) 
        {
            Dash.OnNext(UniRx.Unit.Default);
        } 
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
    }
}
