using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpinnerStateMachine
{

    public Enemy_SpinnerState CurrentEnemyState {  get; set; }

    public void Initialize(Enemy_SpinnerState startingState){
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(Enemy_SpinnerState newState){
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
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
