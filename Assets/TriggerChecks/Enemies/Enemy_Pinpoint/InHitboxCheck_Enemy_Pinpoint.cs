using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHitboxCheck_Enemy_Pinpoint : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    [SerializeField] private Enemy_Pinpoint enemy;
    [SerializeField] private Collider2D enemyHitbox;

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == PlayerTarget){
            StartCoroutine(InflictDamage());
        }
        if(collision.gameObject.CompareTag("PlayerBulletHitbox")){
            StartCoroutine(TakeDamage());
        }

        if (collision.gameObject.CompareTag("LeftBoundary")) {
            enemy.strafeLeft = false;
            enemy.strafeRight = true;
        }
        if (collision.gameObject.CompareTag("RightBoundary")) {
            enemy.strafeLeft = true;
            enemy.strafeRight = false;
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

    public IEnumerator EnableHitbox(){
        yield return new WaitForSeconds(1.5f);
        enemyHitbox.enabled = true;
    }

    void OnEnable(){
        enemyHitbox.enabled = false;
        StartCoroutine(EnableHitbox());
    }
}
