using NF.Main.Core;
using UnityEngine;

public class BaseMovement : MonoExt, IJump, IMove
{
    public float _moveSpeed;
    public float _jumpForce;
    public float _dashForce;
    public float _dashCooldown = 5f;
    public float _currentDashCooldown;
    public float _jumpCooldown;
    public float _airMultiplier;
    public bool _isAbleToJump = true;
    public bool _isAbleToDash = true;
    public bool _isGrounded = true;
    public bool _isDashing = false;
    public Rigidbody _rb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void Jump()
    {
    }

    public virtual void OnLand()
    {
    }

    public virtual void Move(Vector2 movementDireciton)
    {
        
    }

    public virtual void Dash(Vector3 dashDireciton)
    {

    }

    public virtual void ResetDash()
    {

    }

    public virtual void Turn(Vector2 lookDirection)
    {

    }

    public virtual void DashCooldown()
    {
        
    }

    public virtual void ChangeMaxSpeed(float maxSpeedValue)
    {

    }   

    public virtual void ChangeDrag(float dragValue)
    {

    } 

    public virtual void ChangeMoveSpeed(float moveSpeedValue)
    {
        
    }

    public virtual void SpeedControl()
    {
        
    }

    public virtual void RevertDragValue()
    {
        
    }

    public virtual void  CheckIfGround()
    {
        
    }
}
