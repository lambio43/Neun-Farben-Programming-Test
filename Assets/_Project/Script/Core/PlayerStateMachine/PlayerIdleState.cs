using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during idle state
    public class PlayerIdleState: PlayerBaseState
    {
        public PlayerIdleState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
           _playerController._playerMovement._isStrafing = false;
           _playerController._playerMovement._isStrafeJumping = false;
        }

        public override void Update()
        {
            base.Update();
            _playerController._playerMovement.CheckIfGround();
        }
        

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}