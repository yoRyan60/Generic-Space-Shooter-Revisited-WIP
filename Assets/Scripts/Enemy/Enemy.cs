using System.Collections;
using GabrielBigardi.SpriteAnimator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, ITriggerCheckable
{   
    [SerializeField] Rigidbody2D RB;

    //Retrieve object pool methods for handling spawning and despawning.
    public IObjectPool<Enemy> objectPool;
    public IObjectPool<Enemy> ObjectPool { set => objectPool = value; }

    [SerializeField] public bool move = false;
    [SerializeField] public float enemyMovementSpeed = 2f;
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
    }

    private void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    public void ResetEnemy() //Whenever the enemy dies or it's despawn time reaches 0.
    {
        enemyAnimator.Play("Idle");
        move = true;
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
