using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private IObjectPool<Enemy> objectPool;
    //throws an exception if we try to return an existing item that's already in the pool.
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float timeUntilSpawn;
    [SerializeField] float minimumSpawnTime;
    [SerializeField] float maximumSpawnTime;

    float spawnerMovementSpeed = 3f;
    bool strafeLeft = false;
    bool strafeRight = false;

    void Start(){
        StrafeChance();
    }

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

    
    private Enemy CreateEnemy(){
        transform.position = GameObject.Find("EnemySpawner").transform.position;
        Enemy enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    private void OnGetFromPool(Enemy pooledObject){
        pooledObject.transform.position = GameObject.Find("EnemySpawner").transform.position; //Sets the pooled object to the current position of the spawner.
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(Enemy pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
