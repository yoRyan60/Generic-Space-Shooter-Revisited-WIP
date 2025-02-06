using System.Collections;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Spinner: MonoBehaviour, ITriggerCheckable
{   
    #region Object Pool
    public IObjectPool<Enemy_Spinner> objectPool;
    public IObjectPool<Enemy_Spinner> ObjectPool { set => objectPool = value; }
    #endregion

    #region Enemy parameters and SpriteAnimator
    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.
    [SerializeField] public bool move = false;
    [SerializeField] public float enemyMovementSpeed = 2f;
    [SerializeField] private float despawnTimer = 3f;
    #endregion
    
    #region State Machine
    public Enemy_SpinnerStateMachine StateMachine { get; set; }
    public Enemy_SpinnerIdleState IdleState { get; set; }
    public Enemy_SpinnerAlertState  AlertState { get; set; }
    public Enemy_SpinnerAttackState AttackState { get; set; }
    #endregion

    public bool IsInAttackRange { get; set; }

    private void Awake(){

        StateMachine = new Enemy_SpinnerStateMachine();

        IdleState = new Enemy_SpinnerIdleState(this, StateMachine);
        AlertState = new Enemy_SpinnerAlertState(this, StateMachine);
        AttackState = new Enemy_SpinnerAttackState(this, StateMachine);
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
    
    IEnumerator WindUp() {
        enemyAnimator.Play("Prepare");
        yield return new WaitForSeconds(1f);
    }
    public void Alerted() {
        StartCoroutine(WindUp());
    }

    public void Attack(){
        enemyAnimator.Play("Attack");
    }

    public void SetInAttackRange(bool isInAttackRange)
    {
        IsInAttackRange = isInAttackRange;
    }

    void OnEnable(){
        move = true;
    }

    /*public enum AnimationTriggerType{
        DetectedPlayer,
        InitiateAttack
    }*/
}
