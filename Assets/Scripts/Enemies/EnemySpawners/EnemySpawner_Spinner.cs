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
    #endregion
    
    #region Spawner movement parameters like movement speed, strafeLeft and strafeRight
    float spawnerMovementSpeed = 3f;
    bool strafeLeft = false;
    bool strafeRight = false;
    #endregion

    void Start(){
        StrafeChance();
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
        spawnerMovement();
    }

    private void spawnerMovement(){ //Make the spawner move for dynamic spawn locations for the enemies.
        if(strafeLeft) {
            transform.position += new Vector3(-spawnerMovementSpeed * Time.deltaTime, 0, 0);
        }
        if(strafeRight) {
            transform.position += new Vector3(spawnerMovementSpeed * Time.deltaTime, 0, 0);
        }
    }    

    void StrafeChance(){ // similar to the enemies moving left and right, it's just that this never moves from its x-axis.
        float strafeChance = Random.Range(0.0f, 1.0f);
        if(strafeChance > 0.0f && strafeChance <= 0.5f){
            strafeLeft = true;
            strafeRight = false;
        }
        else if (strafeChance > 0.5f && strafeChance <= 1.0f){
            strafeLeft = false;
            strafeRight = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("LeftBoundary")) {
            strafeLeft = false;
            strafeRight = true;
        }
        if (collision.gameObject.CompareTag("RightBoundary")) {
            strafeLeft = true;
            strafeRight = false;
        }
    }
    
    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    
    private Enemy_Spinner CreateEnemy(){
        transform.position = GameObject.FindGameObjectWithTag("EnemySpawner(Spinner)").transform.position;
        Enemy_Spinner enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    private void OnGetFromPool(Enemy_Spinner pooledObject){
        pooledObject.transform.position = GameObject.FindGameObjectWithTag("EnemySpawner(Spinner)").transform.position; //Sets the pooled object to the current position of the spawner.
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(Enemy_Spinner pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy_Spinner pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
