using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRangeCheck_Enemy_Pinpoint : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    [SerializeField] private Enemy_Pinpoint enemy;
    [SerializeField] private Collider2D enemyAttackRange;
    
    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        //enemyAttackRange = GetComponentInChildren<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == PlayerTarget){
            StartCoroutine(playerSpotted());
        }
        else{
            //enemy.SetInAttackRange(false);
        }
    }

    IEnumerator InflictDamage(){
        yield return new WaitForSeconds(0.1f);
        enemy.objectPool.Release(enemy);
        enemy.ResetEnemy();
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }
    public IEnumerator EnableAttackRange(){
        yield return new WaitForSeconds(1f);
        enemyAttackRange.enabled = true;
    }

    IEnumerator playerSpotted(){
        yield return new WaitForSeconds(0.35f);
        enemy.SetInAttackRange(true);
        yield return new WaitForSeconds(0.1f);
        enemy.SetInAttackRange(false);
    }
    
    void OnEnable(){
        enemyAttackRange.enabled = false;
        StartCoroutine(EnableAttackRange());
    }
}
