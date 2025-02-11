using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyBaseState : BaseState
    {
            protected readonly EnemyController _enemyController;
            protected readonly Animator _animator;

            protected static readonly int IdleHash = Animator.StringToHash("Patrol");
    
            protected EnemyBaseState(EnemyController enemyController, Animator animator)
            {
                _enemyController = enemyController;
                _animator = animator;
            }
    }

    public enum EnemyState
    {
        Patrol
    }
}
