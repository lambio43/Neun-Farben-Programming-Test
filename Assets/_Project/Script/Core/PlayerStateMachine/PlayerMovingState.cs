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
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            _playerController._playerMovement.Move(_playerController.GetMovementDirection());
            _playerController._playerMovement.CheckIfGround();
        }


        public override void OnExit()
        {
            base.OnExit();
        }
    }
}