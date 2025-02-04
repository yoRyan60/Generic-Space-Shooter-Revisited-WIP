using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] public SpriteAnimator laserAnimator; //Using the custom sprite animator.
    //Retrieve object pool methods for handling spawning and despawning.
    public IObjectPool<PlayerLaser> objectPool;
    public IObjectPool<PlayerLaser> ObjectPool { set => objectPool = value; }
    //[SerializeField] float bulletSpeed = 4f;
    [SerializeField] float despawnTime = 1f;
    [SerializeField] PlayerBulletSpawner playerBulletSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        laserAnimator.Play("Active");
        despawnTime -= Time.deltaTime;
        transform.position += new Vector3(playerBulletSpawner.transform.position.x, playerBulletSpawner.transform.position.y, 0);
        if(despawnTime <= 0){
            objectPool.Release(this);
            ResetLaser();
        }
    }

    public void ResetLaser(){
        despawnTime = 1f;
    }
}
