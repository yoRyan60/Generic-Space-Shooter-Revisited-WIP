using System.Collections;
using GabrielBigardi.SpriteAnimator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, ITriggerCheckable
{   
    [SerializeField] Rigidbody2D RB;
    [SerializeField] public float enemyMovementSpeed = 2f;

    //Retrieve object pool methods for handling spawning and despawning.
    public IObjectPool<Enemy> objectPool;
    public IObjectPool<Enemy> ObjectPool { set => objectPool = value; }
    //[SerializeField] bool strafeLeft = false;
    //[SerializeField] bool strafeRight = false;
    [SerializeField] public bool move = false;
    [SerializeField] private float despawnTimer = 3f;
    [SerializeField] public SpriteAnimator enemyAnimator; //Using the custom sprite animator.

    //State Machine for the enemy.
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyAlertState  AlertState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsInAttackRange { get; set; }

    private void Awake(){

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        AlertState = new EnemyAlertState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    // Start is called before the first frame update
    void Start()
    {   
        //enemyCollider = GetComponent<Collider2D>();
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        if(StateMachine.CurrentEnemyState == IdleState){
            despawnTimer -= Time.deltaTime;
            if(despawnTimer <= 0){
                objectPool.Release(this);
                StateMachine.ChangeState(IdleState);
                ResetEnemy();
            }
        }
        if(StateMachine.CurrentEnemyState == AttackState){
            despawnTimer -= Time.deltaTime;
            if(despawnTimer <= 0){
                objectPool.Release(this);
                StateMachine.ChangeState(IdleState);
                ResetEnemy();
            }
        }
        /*if(isAlive){
            initiateMovement();
        }*/
    }

    private void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    /*void StrafeChance(){
        float strafeChance = Random.Range(0.0f, 1.0f);
        if(strafeChance > 0.0f && strafeChance <= 0.5f){
            strafeLeft = true;
            strafeRight = false;
        }
        else if (strafeChance > 0.5f && strafeChance <= 1.0f){
            strafeLeft = false;
            strafeRight = true;
        }
    }*/
    
    /*void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("Attack zone entered.");
        if (collision.gameObject.name == "LeftBoundary") {
            strafeLeft = false;
            strafeRight = true;
            //Debug.Log("Collided with Left Boundary!");
        }
        if (collision.gameObject.name == "RightBoundary") {
            strafeLeft = true;
            strafeRight = false;
            //Debug.Log("Collided with Right Boundary!");
        }
    }*/

    /*void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("EnemyDespawnZone")){ 
            // Since I have multiple despawn areas, a tag allows me to easily duplicate them without having to rename them.
            // So even if the despawn areas have different names, so as long as they have the same tag, this will work.
            if(collision.gameObject.CompareTag("EnemyAttackRange")) return;
            if (objectPool == null){
                objectPool.Release(this);
            }
            objectPool.Release(this);
            ResetEnemy();
        }
    }*/

    /*void initiateMovement(){
        if(move) {
            transform.position += new Vector3(0, -enemyMovementSpeed * Time.deltaTime, 0);
        }
        if(strafeLeft) {
            transform.position += new Vector3(-enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
        if(strafeRight) {
            transform.position += new Vector3(enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
    }*/

    public void ResetEnemy() //Once the enemy either dies or reaches the despawn area.
    {
        enemyAnimator.Play("Idle");
        //isAlive = true;
        move = true;
        //strafeLeft = false;
        //strafeRight = false;
        IsInAttackRange = false;
        enemyMovementSpeed = 2.5f;
        despawnTimer = 3f;
    }

    public void AlertedEnemy() {
        StartCoroutine(WindUp());
    }

    public void AttackingEnemy(){
        enemyAnimator.Play("Attack");
    }

    public void SetInAttackRange(bool isInAttackRange)
    {
        IsInAttackRange = isInAttackRange;
    }

    IEnumerator WindUp() {
        enemyAnimator.Play("Prepare");
        yield return new WaitForSeconds(1f);
    }


    public enum AnimationTriggerType{
        DetectedPlayer,
        InitiateAttack
    }
}
