using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy_PinpointAlertState : Enemy_PinpointState
{
    InAttackRangeCheck_Enemy_Pinpoint inAttackRangeCheck_Enemy_Pinpoint;

    public Enemy_PinpointAlertState(Enemy_Pinpoint enemy, Enemy_PinpointStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.StrafeChance();
        enemy.StartCoroutine(prepareToAttack());
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.strafeActive)
        {
            enemy.Strafe();
        }
        if (enemy.IsInAttackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    IEnumerator prepareToAttack(){
        enemy.move = false;
        yield return new WaitForSeconds(1f);
        enemy.enemyMovementSpeed = 2.8f;
        enemy.strafeActive = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
