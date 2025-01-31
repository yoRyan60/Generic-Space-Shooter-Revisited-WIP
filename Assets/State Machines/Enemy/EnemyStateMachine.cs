using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{

    public EnemyState CurrentEnemyState {  get; set; }

    public void Initialize(EnemyState startingState){
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState newState){
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
