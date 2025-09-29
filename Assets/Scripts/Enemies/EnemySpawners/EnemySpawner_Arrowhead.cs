using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner_Arrowhead : MonoBehaviour
{
    #region Object Pool, prefab and parameters such as timers per spawn, capacity
    [SerializeField] private Enemy_Arrowhead enemyPrefab;
    private IObjectPool<Enemy_Arrowhead> objectPool;
    //throws an exception if we try to return an existing item that's already in the pool.
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 8;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float timeUntilSpawn;
    [SerializeField] float minimumSpawnTime;
    [SerializeField] float maximumSpawnTime;
    private int minimumSpawnCount = 1;
    private int maximumSpawnCount = 5;
    [SerializeField] private int spawnCount;
    public Transform[] spawnPoints;
    #endregion

    void Start(){

    }

    void Awake(){
        SetTimeUntilSpawn();
        objectPool = new ObjectPool<Enemy_Arrowhead>(CreateEnemy, OnGetFromPool, OnReleaseFromPool, 
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        for (int i = 0; i < defaultCapacity; i++){
            Enemy_Arrowhead enemy = CreateEnemy();
            objectPool.Release(enemy);
            if (enemy == null){
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0){
            SetEnemySpawnCount();
            for (int i = 0; i < spawnCount; i++)
            {
                objectPool.Get();   
            }
            SetTimeUntilSpawn();
        }
    } 

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void SetEnemySpawnCount()
    {
        spawnCount = Random.Range(minimumSpawnCount, maximumSpawnCount);
    }

    
    private Enemy_Arrowhead CreateEnemy()
    {
        Enemy_Arrowhead enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    private void OnGetFromPool(Enemy_Arrowhead pooledObject){
        pooledObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position; //Sets the pooled object to the current position of the spawner.
        pooledObject.transform.rotation = enemyPrefab.transform.rotation;
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(Enemy_Arrowhead pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy_Arrowhead pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
