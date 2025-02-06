using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_SpinnerAttackState : Enemy_SpinnerState
{   
    private GameObject playerPosition;
    private Vector3 targetPosition;
    private Vector3 currentDirection;

    public Enemy_SpinnerAttackState(Enemy_Spinner enemy, Enemy_SpinnerStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    /*public override void AnimationTriggerEvent(Enemy_Spinner.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }*/

    public override void EnterState()
    {
        base.EnterState();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        enemy.enemyMovementSpeed = 4f;
        if(playerPosition != null){
            currentDirection = (playerPosition.transform.position - enemy.transform.position).normalized;
        }
        enemy.Attack();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(enemy.IsInAttackRange){
            enemy.transform.position += currentDirection * enemy.enemyMovementSpeed * Time.deltaTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
