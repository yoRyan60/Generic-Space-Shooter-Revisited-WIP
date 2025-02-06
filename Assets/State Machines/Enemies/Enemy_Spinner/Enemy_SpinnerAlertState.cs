using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpinnerAlertState : Enemy_SpinnerState
{
    public Enemy_SpinnerAlertState(Enemy_Spinner enemy, Enemy_SpinnerStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    /*public override void AnimationTriggerEvent(Enemy_Spinner.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }*/

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy alerted of player's presence.");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(enemy.IsInAttackRange){
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
