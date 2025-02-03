using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour
{
    //Retrieve object pool methods for handling spawning and despawning.
    public IObjectPool<PlayerBullet> objectPool;
    public IObjectPool<PlayerBullet> ObjectPool { set => objectPool = value; }
    [SerializeField] float bulletSpeed = 4f;
    [SerializeField] float despawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        transform.position += new Vector3(0, bulletSpeed * Time.deltaTime, 0);
        if(despawnTime <= 0){
            objectPool.Release(this);
            ResetBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("EnemyHitbox")){
            objectPool.Release(this);
            ResetBullet();
            Debug.Log("Hit an enemy!");
        }
    }

    public void ResetBullet(){
        despawnTime = 1f;
    }
}
