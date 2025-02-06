using System.Collections;
using System.Collections.Generic;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pinpoint : MonoBehaviour
{
    #region Object Pool
    public IObjectPool<Enemy_Pinpoint> objectPool;
    public IObjectPool<Enemy_Pinpoint> ObjectPool { set => objectPool = value; }
    #endregion

    #region Enemy parameters and SpriteAnimator
    public SpriteAnimator enemyAnimator; //Using the custom sprite animator.
    [SerializeField] public bool move = false;
    [SerializeField] public float enemyMovementSpeed = 2f;
    [SerializeField] private float despawnTimer = 3f;
    [SerializeField] private bool strafeLeft = false;
    [SerializeField] private bool strafeRight = false;
    #endregion
    
    #region State Machine
    public Enemy_SpinnerStateMachine StateMachine { get; set; }
    public Enemy_SpinnerIdleState IdleState { get; set; }
    public Enemy_SpinnerAlertState  AlertState { get; set; }
    public Enemy_SpinnerAttackState AttackState { get; set; }
    #endregion

    public bool IsInAttackRange { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
