using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_PinpointAttackState : Enemy_PinpointState
{
    private GameObject playerPosition;
    private Vector3 targetPosition;
    private Vector3 currentDirection;

    public bool hasFired = false;

    public Enemy_PinpointAttackState(Enemy_Pinpoint enemy, Enemy_PinpointStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    public override void EnterState()
    {
        base.EnterState();
        //Debug.Log("Player spotted");
        enemy.strafeActive = false;
        hasFired = false;
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        if (playerPosition != null)
        {
            currentDirection = (playerPosition.transform.position - enemy.transform.position).normalized;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.IsInAttackRange && !hasFired)
        {
            hasFired = true;
            enemy.Attack();
            enemy.StartCoroutine(attackCooldown());
        }
    }

    IEnumerator attackCooldown(){
        yield return new WaitForSeconds(0.25f);
        enemy.StateMachine.ChangeState(enemy.AlertState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
