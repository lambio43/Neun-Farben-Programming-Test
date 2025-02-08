using System.Collections;
using UniRx.Triggers;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    public bool _isDashing = false;

    public Transform _orientation;

    private Vector3 _moveDirection;

    public float _groundDrag;
    public float _playerHeight;
    public LayerMask _groundLayerMask;




    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SpeedControl();
    }

    public override void Jump()
    {
        if(_isAbleToJump)
        {
            _rb.linearDamping = 0;
            //_rb.linearVelocity = new Vector3 (_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isAbleToJump = false;
            //Invoke(nameof(OnLand), _jumpCooldown);
        }   
    }

    public override void OnLand()
    {
        _isAbleToJump = true;
    }

    public override void Move(Vector2 movementDireciton)
    {
        _moveDirection = _orientation.forward * movementDireciton.y + _orientation.right * movementDireciton.x;
        
        if(_isGrounded == true)
        {
            _rb.linearDamping = _groundDrag;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force); 
        }
        else
        {
            _rb.linearDamping = 0;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * _airMultiplier * 10f, ForceMode.Force);
        }

        
        //_rb.MovePosition(_rb.position + (_moveDirection.normalized  * _moveSpeed * Time.deltaTime));
    }

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

    public override void ResetDash()
    {
        base.ResetDash();
        _isAbleToDash = true;
        _isDashing = false;
        SpeedControl();
    }

    public void CheckIfGround()
    {
        //ground check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask);
        _isAbleToJump = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayerMask);
    }

    public void SpeedControl()
    {
        // Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

        // if(flatVel.magnitude > _moveSpeed)
        // {
        //     Vector3 limitedVel = flatVel.normalized * _moveSpeed;
        //     _rb.linearVelocity = new Vector3 (limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        // }
        _rb.maxLinearVelocity = _moveSpeed;
    }

    private IEnumerator CO_DelayedForce(Vector3 direction)
    {
        yield return new WaitForSeconds(0.025f);
        _rb.maxLinearVelocity = _dashForce;
        _rb.AddForce(_dashForce * direction, ForceMode.Impulse);
    }
}
