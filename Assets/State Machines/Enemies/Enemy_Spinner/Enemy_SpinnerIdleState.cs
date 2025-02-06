using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpinnerIdleState : Enemy_SpinnerState
{
    public Enemy_SpinnerIdleState(Enemy_Spinner enemy, Enemy_SpinnerStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    /*public override void AnimationTriggerEvent(Enemy_Spinner.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }*/

    public override void EnterState()
    {
        base.EnterState();
        enemy.ResetEnemy();
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
        if(enemy.IsInAttackRange){
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
