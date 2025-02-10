
using UnityEngine;

using NF.Main.Core.EnemyStateMachine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
    {  
    }

    public override void OnEnter()
    {
        base.OnEnter(); 
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _enemyController._enemyMovement.Move(Vector2.zero);
    }


    public override void OnExit()
    {

    }
}