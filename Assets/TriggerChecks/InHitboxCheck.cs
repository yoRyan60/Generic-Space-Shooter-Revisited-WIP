using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class InHitboxCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    [SerializeField] private Enemy_Spinner enemy;

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        //enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == PlayerTarget){
            StartCoroutine(InflictDamage());
        }
        if(collision.gameObject.CompareTag("PlayerBulletHitbox")){
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator InflictDamage(){
        yield return new WaitForSeconds(0.1f);
        enemy.objectPool.Release(enemy);
        enemy.ResetEnemy();
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }

    IEnumerator TakeDamage(){
        yield return new WaitForSeconds(0.00012f);
        enemy.objectPool.Release(enemy);
        enemy.ResetEnemy();
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
