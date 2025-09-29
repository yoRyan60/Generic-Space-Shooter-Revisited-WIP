using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletSpawner : MonoBehaviour
{
    #region EnemyBullet prefab and parameters
    private IObjectPool<EnemyBullet> objectPool;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    [SerializeField] public SpriteAnimator bulletAnimator; //Using the custom sprite animator.
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 1;
    [SerializeField] private int maxSize = 2;
    public bool hasFired = false;
    #endregion

    [SerializeField] GameObject enemyBulletSpawner;

    void Awake(){
        objectPool = new ObjectPool<EnemyBullet>(CreateEnemyBullet, OnGetFromPool, OnReleaseFromPool, 
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        for (int i = 0; i < defaultCapacity; i++){
            EnemyBullet enemyBullet = CreateEnemyBullet();
            objectPool.Release(enemyBullet);
            if (enemyBullet == null){
                return;
            }
        }
      //for (int i = 0; i < defaultCapacity; i++){
      //    Enemy enemy = objectPool.Get();
      //    objectPool.Release(enemy);
      //}
    }

    public void Shoot(){
        objectPool.Get();
        StartCoroutine(cooldownTimer());
    }

    IEnumerator cooldownTimer(){
        hasFired = true;
        yield return new WaitForSeconds(1f);
        hasFired = false;
    }

    private EnemyBullet CreateEnemyBullet(){
        transform.position = enemyBulletSpawner.transform.position;
        EnemyBullet enemyBulletInstance = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBulletInstance.ObjectPool = objectPool;
        return enemyBulletInstance;
    }

    private void OnGetFromPool(EnemyBullet pooledObject){
        pooledObject.transform.position = enemyBulletSpawner.transform.position; //Sets the pooled object to the current position of the spawner.
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(EnemyBullet pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(EnemyBullet pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
