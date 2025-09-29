using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    //Retrieve object pool methods for handling spawning and despawning.
    public IObjectPool<EnemyBullet> objectPool;
    public IObjectPool<EnemyBullet> ObjectPool { set => objectPool = value; }
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] float despawnTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        transform.position += new Vector3(0, -bulletSpeed * Time.deltaTime, 0);
        if(despawnTime <= 0){
            ResetBullet();
            objectPool.Release(this);
        }
    }
    public void fireBullet(){
        objectPool.Get();
    }
    public void ResetBullet(){
        despawnTime = 1.5f;
    }
}
