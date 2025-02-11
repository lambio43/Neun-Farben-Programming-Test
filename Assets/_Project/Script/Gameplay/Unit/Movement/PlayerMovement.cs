using System.Collections;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    public bool _isDashing = false;

    public Transform _orientation;

    private Vector3 _moveDirection;

    public float _groundDrag;
    public float _playerHeight;
    public LayerMask _groundLayerMask;
    private float _dragValueToUse;

    public bool _isStrafing;
    public bool _isStrafeJumping;

    public bool _jumpQueue = false;
    public bool _wishJump = false;
    
    public float _gravity;

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

    public void GravityForce()
    {
        if(_isDashing)
        {
            return;
        }
        _rb.AddForce((Vector3.down * _gravity) * Time.deltaTime, ForceMode.VelocityChange);
    }

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

    public override void Jump()
    {
        //JumpQueue();

        if(_wishJump)// && _isGrounded)
        {
            _wishJump = false;
            _rb.linearDamping = 0;
            //_rb.linearVelocity = new Vector3 (_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            _isAbleToJump = false;
            //Invoke(nameof(OnLand), _jumpCooldown);

            if(_isStrafing)
            {
                _isStrafeJumping = true;
                _rb.maxLinearVelocity = 35f;
            }
            else
            {
                _isStrafeJumping = false;
            }
        }   
    }

    public override void Move(Vector2 movementDireciton)
    {
        if(movementDireciton.x != 0)
        {
            _isStrafing = true;
        }
        else
        {
            _isStrafing = false;
            _isStrafeJumping = false;
        }

        _moveDirection = _orientation.forward * movementDireciton.y + _orientation.right * movementDireciton.x;

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
            //_gravity = 15;
            
        }
        else
        {
            _rb.linearDamping = 0;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed  * 10f, ForceMode.Force);
            SpeedControl();
        }
    }

    public override void Dash(Vector3 dashDireciton)
    {
        if(_isAbleToDash)
        {
            _rb.linearDamping = 0;
            //_gravity = 15;
            _isDashing = true;
            base.Dash(dashDireciton);
            _isAbleToDash = false;
            StartCoroutine(CO_DelayedForce(dashDireciton));
            Invoke(nameof(ResetDash), _dashDuration);
        }
    }

    public override void ResetDash()
    {
        base.ResetDash();
        _isAbleToDash = true;
        _isDashing = false;
        SpeedControl();
    }

    public override void CheckIfGround()
    {
        //ground check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask);
        _isAbleToJump = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask); 
        
        
    }

    public override void SpeedControl()
    {
        _rb.maxLinearVelocity = _moveSpeed;
    }

    private IEnumerator CO_DelayedForce(Vector3 direction)
    {
        yield return new WaitForSeconds(0.025f);
        _rb.maxLinearVelocity = _dashForce;
        _rb.AddForce(_dashForce * direction, ForceMode.VelocityChange);
    }
    
    public override void ChangeDrag(float dragValue)
    {
        base.ChangeDrag(dragValue);
        _dragValueToUse = dragValue;
        _rb.linearDamping = dragValue;
    }

    public override void RevertDragValue()
    {
        base.RevertDragValue();
        _dragValueToUse = _groundDrag;
    }

    public override void ChangeMaxSpeed(float maxSpeedValue)
    {
        base.ChangeMaxSpeed(maxSpeedValue);
        _rb.maxLinearVelocity = maxSpeedValue;
    }

    public override void ChangeMoveSpeed(float moveSpeedValue)
    {
        base.ChangeMoveSpeed(moveSpeedValue);
        _moveSpeed = moveSpeedValue;
        SpeedControl();
    }
}
