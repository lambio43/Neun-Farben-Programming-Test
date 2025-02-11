using System.Collections;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    public bool _isDashing = false;

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
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SpeedControl();
        _dragValueToUse = _groundDrag;
    }

    private void FixedUpdate()
    {
        GravityForce();
    }

    //Gravity force
    public void GravityForce()
    {
        if(_isDashing)
        {
            return;
        }
        _rb.AddForce((Vector3.down * _gravity) * Time.deltaTime, ForceMode.VelocityChange);
    }

    //Determine if player is trying to jump again
    public void JumpQueue()
    {
        if (_isGrounded)
		{
			_wishJump = true;
		}

		if (!_isGrounded)
		{
			_jumpQueue = true;
		}
		if (_isGrounded && _jumpQueue)
		{
			_wishJump = true;
			_jumpQueue = false;
		}

        if(_wishJump)
        {
            Jump();
        }
    }

    //Jump
    public override void Jump()
    {
        if(_wishJump)
        {
            _wishJump = false;
            _rb.linearDamping = 0;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            _isAbleToJump = false;

            if(_isStrafing)
            {
                _isStrafeJumping = true;
                _rb.maxLinearVelocity = 30f;
            }
            else
            {
                _isStrafeJumping = false;
            }
        }   
    }


    public override void Move(Vector2 movementDireciton)
    {
        //Determine whether player is strafing
        if(movementDireciton.x != 0)
        {
            _isStrafing = true;
        }
        else
        {
            _isStrafing = false;
            _isStrafeJumping = false;
        }

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
        else if (_isGrounded == false && _isStrafing == true)
        {
            _rb.linearDamping = 0;
            _rb.AddForce(_orientation.forward * _moveSpeed * 3.5f, ForceMode.Acceleration);
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * _airMultiplier * 2.5f, ForceMode.Acceleration);     
        }
        else
        {
            _rb.linearDamping = 0;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed  * 10f, ForceMode.Force);
            SpeedControl();
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
            StartCoroutine(CO_DelayedForce(dashDireciton));
            Invoke(nameof(ResetDash), _dashDuration);
        }
    }

    //Make player able to dash again
    public override void ResetDash()
    {
        base.ResetDash();
        _isAbleToDash = true;
        _isDashing = false;
        SpeedControl();
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
        _rb.maxLinearVelocity = _dashForce;
        _rb.AddForce(_dashForce * direction, ForceMode.VelocityChange);
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
}
