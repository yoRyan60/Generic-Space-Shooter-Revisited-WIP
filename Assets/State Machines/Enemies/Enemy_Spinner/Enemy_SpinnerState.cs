using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using GabrielBigardi.SpriteAnimator;

public class Enemy_SpinnerState
{
    protected Enemy_Spinner enemy;
    protected Enemy_SpinnerStateMachine enemyStateMachine;

    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.

    public Enemy_SpinnerState(Enemy_Spinner enemy, Enemy_SpinnerStateMachine enemyStateMachine){
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

    /*public virtual void AnimationTriggerEvent(Enemy_Spinner.AnimationTriggerType triggerType){

    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
