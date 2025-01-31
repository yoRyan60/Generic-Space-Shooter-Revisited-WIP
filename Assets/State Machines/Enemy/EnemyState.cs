using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using GabrielBigardi.SpriteAnimator;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine){
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

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType){

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
