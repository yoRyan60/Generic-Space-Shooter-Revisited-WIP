using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pinpoint : MonoBehaviour
{
    #region Object Pool
    public IObjectPool<Enemy_Pinpoint> objectPool;
    public IObjectPool<Enemy_Pinpoint> ObjectPool { set => objectPool = value; }
    public IObjectPool<EnemyBullet> objectPool_enemyBullet;
    public IObjectPool<EnemyBullet> ObjectPool_enemyBullet { set => objectPool_enemyBullet = value; }
    #endregion

    #region Enemy parameters and SpriteAnimator
    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.
    [SerializeField] public bool move = false;
    [SerializeField] public float enemyMovementSpeed = 4f;
    [SerializeField] private float despawnTimer = 3f;
    [SerializeField] public float travelTimer;
    [SerializeField] public float maxTravelTime;
     [SerializeField] public float minTravelTime;
    [SerializeField] public bool strafeLeft = false;
    [SerializeField] public bool strafeRight = false;
    [SerializeField] public bool strafeActive = false;
    [SerializeField] public float attackDuration = 10f;
    [SerializeField] EnemyBulletSpawner enemyBulletSpawner;
    #endregion
    
    #region State Machine
    public Enemy_PinpointStateMachine StateMachine { get; set; }
    public Enemy_PinpointIdleState IdleState { get; set; }
    public Enemy_PinpointAlertState  AlertState { get; set; }
    public Enemy_PinpointAttackState AttackState { get; set; }
    #endregion

    public bool IsInAttackRange { get; set; }
    
    private void Awake(){

        StateMachine = new Enemy_PinpointStateMachine();

        IdleState = new Enemy_PinpointIdleState(this, StateMachine);
        AlertState = new Enemy_PinpointAlertState(this, StateMachine);
        AttackState = new Enemy_PinpointAttackState(this, StateMachine);
    }

    // Start is called before the first frame update
    void Start(){
        StateMachine.Initialize(IdleState);
    }

    public void setTravelDistance(){
        travelTimer = Random.Range(minTravelTime, maxTravelTime);
    }
    // Update is called once per frame
    void Update()
    {
        attackDuration -= Time.deltaTime;
        travelTimer -= Time.deltaTime;
        StateMachine.CurrentEnemyState.FrameUpdate();
        if (StateMachine.CurrentEnemyState == IdleState)
        {
            travelTimer -= Time.deltaTime;
            ResetEnemy();
            //despawnTimer -= Time.deltaTime;
            /*if(despawnTimer <= 0){
                objectPool.Release(this);
                StateMachine.ChangeState(IdleState);
                ResetEnemy();
            }*/
        }
        if (attackDuration <= 0)
        {
            StopAttacking();
        }
    }

    private void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void StrafeChance(){ // similar to the enemies moving left and right, it's just that this never moves from its x-axis.
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

    public void Strafe(){ //Make the spawner move for dynamic spawn locations for the enemies.
        if(strafeLeft) {
            transform.position += new Vector3(-enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
        if(strafeRight) {
            transform.position += new Vector3(enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
    }    

    public void ResetEnemy() //Whenever the enemy dies or it's despawn time reaches 0.
    {
        enemyAnimator.Play("Idle");
        move = true;
        IsInAttackRange = false;
        strafeLeft = false;
        strafeRight = false;
        strafeActive = false;
        enemyMovementSpeed = 4f;
        attackDuration = 10f;
        despawnTimer = 3f;
        StateMachine.ChangeState(IdleState);
        //despawnTimer = 3f;
    }
    public void SetInAttackRange(bool isInAttackRange)
    {
        IsInAttackRange = isInAttackRange;
    }

    public void StopAttacking()
    {
        travelTimer = 3f;
        move = false;
        strafeActive = false;
        transform.position += new Vector3(0, -enemyMovementSpeed * Time.deltaTime, 0);
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0){
            objectPool.Release(this);
            ResetEnemy();
        }
    }

    public void Attack()
    {
        StartCoroutine(AttackActive());
        if (attackDuration <= 0)
        {
            StopCoroutine(AttackActive());
        }
    }
    public void PreparetoAttack(){
        StartCoroutine(WindUp());
    }
    IEnumerator WindUp() {
        move = false;
        enemyAnimator.Play("Prepare");
        yield return new WaitForSeconds(1f);
        enemyAnimator.Play("Attack");
        yield return new WaitForSeconds(0.2f);
        strafeActive = true;
    }

    IEnumerator AttackActive()
    {
        yield return new WaitForSeconds(0.5f);
        enemyBulletSpawner.Shoot();
    }
 
    void OnEnable(){
        move = true;
        setTravelDistance();
    }
}
