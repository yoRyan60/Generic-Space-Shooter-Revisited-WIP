using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRangeCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy enemy;

    private bool isWindingUp = false; //to prevent the animation from triggering if the player re-enters the attack range.

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        enemy = GetComponentInParent<Enemy>();
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

    private void OnEnable(){
        isWindingUp = false;
    }
}
