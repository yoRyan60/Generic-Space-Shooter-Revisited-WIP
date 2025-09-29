using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using GabrielBigardi.SpriteAnimator;

public class Enemy_PinpointIdleState : Enemy_PinpointState
{
    SpriteAnimator enemyAnimator;
    public Enemy_PinpointIdleState(Enemy_Pinpoint enemy, Enemy_PinpointStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(enemy.move){
            enemy.transform.position += new Vector3(0, -enemy.enemyMovementSpeed * Time.deltaTime, 0);
        }
        if(enemy.travelTimer <= 0){
            enemy.move = false;
            enemy.PreparetoAttack();
            enemy.StateMachine.ChangeState(enemy.AlertState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
