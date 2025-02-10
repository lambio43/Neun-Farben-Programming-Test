using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyBaseState : BaseState
    {
            protected readonly EnemyController _enemyController;
            protected readonly Animator _animator;

            protected static readonly int IdleHash = Animator.StringToHash("Idle");
            protected static readonly int AttackHash = Animator.StringToHash("Attack");
            protected static readonly int MovingHash = Animator.StringToHash("Moving");
            protected static readonly int HitHash = Animator.StringToHash("Hit");
            protected static readonly int DeathHash = Animator.StringToHash("Death");

            protected EnemyBaseState(EnemyController enemyController, Animator animator)
            {
                _enemyController = enemyController;
                _animator = animator;
            }
    }

    public enum EnemyState
    {
        Patrol,

    }
}
