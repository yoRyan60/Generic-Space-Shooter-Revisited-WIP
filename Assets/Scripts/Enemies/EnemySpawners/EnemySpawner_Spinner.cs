using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner_Spinner : MonoBehaviour
{
    #region Object Pool, prefab and parameters such as timers per spawn, capacity
    [SerializeField] private Enemy_Spinner enemyPrefab;
    private IObjectPool<Enemy_Spinner> objectPool;
    //throws an exception if we try to return an existing item that's already in the pool.
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float timeUntilSpawn;
    [SerializeField] float minimumSpawnTime;
    [SerializeField] float maximumSpawnTime;
    [SerializeField] Vector3 spawnerPosition;
    public Transform[] spawnPoints;
    #endregion

    #region Spawner movement parameters like movement speed, strafeLeft and strafeRight
    public float spawnerMovementSpeed = 3f;
    bool strafeLeft = false;
    bool strafeRight = false;
    #endregion

    void Start(){
        transform.position = spawnerPosition;
    }

    void Awake(){
        SetTimeUntilSpawn();
        objectPool = new ObjectPool<Enemy_Spinner>(CreateEnemy, OnGetFromPool, OnReleaseFromPool, 
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        for (int i = 0; i < defaultCapacity; i++){
            Enemy_Spinner enemy = CreateEnemy();
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
            objectPool.Get();
            SetTimeUntilSpawn();
        }
    } 

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    
    private Enemy_Spinner CreateEnemy(){
        //transform.position = enemyPrefab.transform.position;
        Enemy_Spinner enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    private void OnGetFromPool(Enemy_Spinner pooledObject){
        pooledObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position; //Sets the pooled object to the current position of the spawner.
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(Enemy_Spinner pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy_Spinner pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
