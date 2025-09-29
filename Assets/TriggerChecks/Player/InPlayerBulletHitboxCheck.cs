using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerBulletHitboxCheck : MonoBehaviour
{
    //public GameObject EnemyTarget { get; set; }
    [SerializeField] private PlayerBullet playerBullet;

    void Awake(){
         ///EnemyTarget = GameObject.FindGameObjectWithTag("EnemyHitbox");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("EnemyHitbox")){
            if(playerBullet == null){
                return;
            }
            StartCoroutine(InflictDamage());
        }
    }

    IEnumerator InflictDamage(){
        yield return new WaitForSeconds(0.01f);
        playerBullet.objectPool.Release(playerBullet);
        playerBullet.ResetBullet();
    }
}
