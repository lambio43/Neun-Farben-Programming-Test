using NF.Main.Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace NF.Main.Core.GameStateMachine
{   
    public class GameWinState : GameBaseState
    {
        

        public GameWinState(GameManager gameManager, GameState gameState) : base(gameManager, gameState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game Win state");
            _gameManager.OnWin.Invoke();
        }
    }
}
