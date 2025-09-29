using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PinpointState
{
    protected Enemy_Pinpoint enemy;
    protected Enemy_PinpointStateMachine enemyStateMachine;

    public Enemy_PinpointState(Enemy_Pinpoint enemy, Enemy_PinpointStateMachine enemyStateMachine){
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState(){

    }

    public virtual void ExitState(){

    }

    public virtual void FrameUpdate(){

    }

    public virtual void PhysicsUpdate(){

    }

    /*public virtual void AnimationTriggerEvent(Enemy_Pinpoint.AnimationTriggerType triggerType){

    }*/
}
