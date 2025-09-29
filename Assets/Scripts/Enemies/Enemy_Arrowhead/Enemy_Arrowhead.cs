using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GabrielBigardi.SpriteAnimator;
using UnityEngine.Pool;

public class Enemy_Arrowhead : MonoBehaviour
{
    #region Object Pool
    public IObjectPool<Enemy_Arrowhead> objectPool;
    public IObjectPool<Enemy_Arrowhead> ObjectPool { set => objectPool = value; }
    #endregion

    #region Enemy parameters and SpriteAnimator
    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.
    [SerializeField] public bool move = false;
    [SerializeField] public float enemyMovementSpeed = 2f;
    [SerializeField] private float despawnTimer = 4f;
    #endregion

    // Update is called once per frame
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            objectPool.Release(this);
            ResetEnemy();
        }
        if (move)
        {
            transform.position += new Vector3(0, -enemyMovementSpeed * Time.deltaTime, 0);
        }
    }
    public void ResetEnemy() //Whenever the enemy dies or it's despawn time reaches 0.
    {
        enemyAnimator.Play("Idle");
        move = true;
        enemyMovementSpeed = 2f;
        despawnTimer = 4f;
    }
    void OnEnable(){
        move = true;
    }

}

