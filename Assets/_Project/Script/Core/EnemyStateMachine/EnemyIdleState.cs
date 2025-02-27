
using UnityEngine;

using NF.Main.Core.EnemyStateMachine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
    {  
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.CrossFade(IdleHash, 0.25f); 
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _enemyController._enemyMovement.Turn(Vector2.zero);
        
        if(_enemyController._enemyMovement.IsFacingTarget())
        {
            _enemyController.EnemyState = EnemyState.Patrol;
        }
    }


    public override void OnExit()
    {

    }
}