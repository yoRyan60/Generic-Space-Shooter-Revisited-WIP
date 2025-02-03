using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRangeCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy enemy;
    private Collider2D enemyHitbox;
    private bool isWindingUp = false; //to prevent the animation from triggering if the player re-enters the attack range.

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        enemy = GetComponentInParent<Enemy>();
        enemyHitbox = GetComponentInChildren<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == PlayerTarget && !isWindingUp){
            isWindingUp = true;
            if(isWindingUp){
                StartCoroutine(WindUp());
            }
        }
    }
    IEnumerator WindUp() {
        enemy.move = false;
        enemy.enemyAnimator.Play("Prepare");
        yield return new WaitForSeconds(0.5f);
        enemy.SetInAttackRange(true);
    }

    IEnumerator EnableAttackRange(){
        yield return new WaitForSeconds(0.8f);
        enemyHitbox.enabled = true;
    }

    private void OnEnable(){
        isWindingUp = false;
        enemyHitbox.enabled = false;
        StartCoroutine(EnableAttackRange());
    }
}
