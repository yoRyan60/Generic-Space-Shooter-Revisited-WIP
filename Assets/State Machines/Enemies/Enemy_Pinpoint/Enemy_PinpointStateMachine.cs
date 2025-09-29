using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PinpointStateMachine
{
    public Enemy_PinpointState CurrentEnemyState {  get; set; }

    public void Initialize(Enemy_PinpointState startingState){
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(Enemy_PinpointState newState){
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}
