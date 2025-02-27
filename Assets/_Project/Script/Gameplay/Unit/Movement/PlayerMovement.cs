using System.Collections;
using UniRx.Triggers;
using Unity.Mathematics;
using Unity.Cinemachine;
using UnityEngine;
using UniRx;

public class PlayerMovement : BaseMovement
{
    // To be used for updating cooldown in UI
    public Subject<float> CooldownReduced;

    //Directions
    public Transform _orientation;
    private Vector3 _moveDirection;

    //Physics applied to player
    public float _groundDrag;
    public float _playerHeight;
    public LayerMask _groundLayerMask;
    public float _gravity;
    private float _dragValueToUse;

    //Strafe jumping or bhop variables
    public bool _isStrafing;
    public bool _isStrafeJumping;
    public bool _jumpQueue = false;
    public bool _wishJump = false;

    //player and camera rotation variables
    [SerializeField] private CinemachineCamera _playerCamera;
    private float _xRotation;
    private float _yRotation;

    private void Awake()
    {
        CooldownReduced = new Subject<float>();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SpeedControl();
        _dragValueToUse = _groundDrag;
        OnSubscriptionSet();
    }

    private void FixedUpdate()
    {
        GravityForce();
        DashCooldown();
    }

    //Gravity force
    public void GravityForce()
    {
        _rb.AddForce((Vector3.down * _gravity) * Time.deltaTime, ForceMode.VelocityChange);
    }

    //Jump
    public override void Jump()
    {
        if (_isGrounded)
	 	{
            _rb.linearDamping = 0;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
        }   
    }

    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(CooldownReduced, UIManager.Instance.UpdateDashCoolDownText);
    }

    public override void Move(Vector2 movementDireciton)
    {
        //Move direction
        _moveDirection = _orientation.forward * movementDireciton.y + _orientation.right * movementDireciton.x;
        

        //Check if player is strafe jumping and decide if velocity is limited
        if(_isGrounded == true && _jumpQueue == false)
        {
            _rb.linearDamping = _dragValueToUse;
            _gravity = 20;
        }
        
        if(_isGrounded == true)
        {
            _rb.AddForce( _moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            _rb.linearDamping = 0;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * _airMultiplier  * 10f, ForceMode.Force);
        }
    }

    // Dash
    public override void Dash(Vector3 dashDireciton)
    {
        if(_isAbleToDash)
        {
            _rb.linearDamping = 0;
            _isDashing = true;
            base.Dash(dashDireciton);
            _isAbleToDash = false;
            _currentDashCooldown = _dashCooldown;
            StartCoroutine(CO_DelayedForce(dashDireciton));
            Debug.Log("Dash disable");
        }
    }

    //Make player able to dash again
    public override void ResetDash()
    {
        base.ResetDash();
        _isAbleToDash = true;
        _currentDashCooldown = 0;
        CooldownReduced.OnNext(_currentDashCooldown);
        Debug.Log("Dash Enable");
    }

    public override void DashCooldown()
    {
        base.DashCooldown();
        if (_isAbleToDash)
        {
            return;
        }

        _currentDashCooldown -= Time.deltaTime;
        Debug.Log(_currentDashCooldown);
        CooldownReduced.OnNext(_currentDashCooldown);

        if(_currentDashCooldown <= 0)
        {
            ResetDash();
        }
    }

    // Ground Check
    public override void CheckIfGround()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask);
        _isAbleToJump = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask);
    }

    //Control Max velocity
    public override void SpeedControl()
    {
        _rb.maxLinearVelocity = _moveSpeed;
    }

    //Make dash force have a delay force
    private IEnumerator CO_DelayedForce(Vector3 direction)
    {
        yield return new WaitForSeconds(0.025f);
        _rb.maxLinearVelocity = _dashForce * 0.45f;
        _rb.AddForce(_dashForce * direction, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.5f);
        _isDashing = false;
        SpeedControl();
    }
    
    //Change Drag if needed
    public override void ChangeDrag(float dragValue)
    {
        base.ChangeDrag(dragValue);
        _dragValueToUse = dragValue;
        _rb.linearDamping = dragValue;
    }

    //Revert drag to original value
    public override void RevertDragValue()
    {
        base.RevertDragValue();
        _dragValueToUse = _groundDrag;
    }

    //Change Max Speed if needed
    public override void ChangeMaxSpeed(float maxSpeedValue)
    {
        base.ChangeMaxSpeed(maxSpeedValue);
        _rb.maxLinearVelocity = maxSpeedValue;
    }

    //Change Move speed if needed
    public override void ChangeMoveSpeed(float moveSpeedValue)
    {
        base.ChangeMoveSpeed(moveSpeedValue);
        _moveSpeed = moveSpeedValue;
        SpeedControl();
    }

    //player turn camera and plaeyr
    public override void Turn(Vector2 LookDirection)
    {
        _yRotation += LookDirection.x;

        _xRotation -= LookDirection.y;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
        _playerCamera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
