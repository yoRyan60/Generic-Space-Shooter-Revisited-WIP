using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletSpawner : MonoBehaviour
{
    private IObjectPool<PlayerBullet> objectPool;
    [SerializeField] private PlayerBullet playerBulletPrefab;
    [SerializeField] public SpriteAnimator bulletAnimator; //Using the custom sprite animator.

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 3;
    [SerializeField] private int maxSize = 6;
    public bool hasFired = false;
    Player player;

    void Start(){
        
    }

    void Awake(){
        objectPool = new ObjectPool<PlayerBullet>(CreatePlayerBullet, OnGetFromPool, OnReleaseFromPool, 
        OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        for (int i = 0; i < defaultCapacity; i++){
            PlayerBullet playerBullet = CreatePlayerBullet();
            objectPool.Release(playerBullet);
            if (playerBullet == null){
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

    }

    void OnTriggerEnter2D(Collider2D collision){

    }
    public void Shoot(){
        objectPool.Get();
        StartCoroutine(cooldownTimer());
    }

    IEnumerator cooldownTimer(){
        hasFired = true;
        yield return new WaitForSeconds(0.5f);
        hasFired = false;
    }

    private PlayerBullet CreatePlayerBullet(){
        transform.position = GameObject.Find("PlayerBulletSpawner").transform.position;
        PlayerBullet playerBulletInstance = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        playerBulletInstance.ObjectPool = objectPool;
        return playerBulletInstance;
    }

    private void OnGetFromPool(PlayerBullet pooledObject){
        pooledObject.transform.position = GameObject.Find("PlayerBulletSpawner").transform.position; //Sets the pooled object to the current position of the spawner.
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(PlayerBullet pooledObject){
        pooledObject?.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(PlayerBullet pooledObject){
        Destroy(pooledObject.gameObject);
    }
}
