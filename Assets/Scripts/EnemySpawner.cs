using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private IObjectPool<Enemy> objectPool;

    private Vector3 spawnPosition;

    //throws an exception if we try to return an existing item that's already in the pool.
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float timeUntilSpawn;
    [SerializeField] float minimumSpawnTime;
    [SerializeField] float maximumSpawnTime;

    void Awake(){
        SetTimeUntilSpawn();
        objectPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnReleaseFromPool, 
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        for (int i = 0; i < defaultCapacity; i++){
            Enemy enemy = CreateEnemy();
            objectPool.Release(enemy);
            if (enemy == null){
                return;
            }
        }
      //for (int i = 0; i < defaultCapacity; i++){
      //    Enemy enemy = objectPool.Get();
      //    objectPool.Release(enemy);
      //}
    }


    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0){
            objectPool.Get();
            SetTimeUntilSpawn();
        }
    }    
    
    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    
    private Enemy CreateEnemy(){
        Enemy enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    private void OnGetFromPool(Enemy pooledObject){
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(Enemy pooledObject){
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
