using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerMovingState: PlayerBaseState
    {
        public PlayerMovingState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //Use this for transitioning between different animator hashes
            //_animator.CrossFade(IdleHash, 0.5f);
            
            //Debug.Log("Entering Player Moving State");
        }

        public override void Update()
        {
            base.Update();
            
            //_playerController._playerMovement.SpeedControl();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            _playerController._playerMovement.Move(_playerController.GetMovementDirection());
            _playerController._playerMovement.CheckIfGround();
            //Debug.Log(_playerController._playerMovement._rb.linearVelocity); 
            //_playerController._playerMovementStrafe.CheckIfGround();
            //_playerController._playerMovementStrafe.Move(_playerController.GetMovementDirection());
        }


        public override void OnExit()
        {
            base.OnExit();
            //Debug.Log("Exiting Player Moving State");
        }
    }
}