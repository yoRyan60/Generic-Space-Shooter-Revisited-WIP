using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{   
    [SerializeField] Rigidbody2D RB;
    [SerializeField] float enemyMovementSpeed = 1f;
    [SerializeField]  bool isAlive = true;

    private IObjectPool<Enemy> objectPool;
    public IObjectPool<Enemy> ObjectPool { set => objectPool = value; }
    public GameObject enemy;
    GameObject enemySpawner;

    [SerializeField] Collider2D enemyCollider;
    [SerializeField] bool strafeLeft = false;
    [SerializeField] bool strafeRight = false;
    [SerializeField] bool move = false;

    // Start is called before the first frame update
    void Start()
    {   
        enemyCollider = GetComponent<Collider2D>();
        enemySpawner = GameObject.Find("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            initiateMovement();
            move = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "LeftBoundary") {
            strafeLeft = false;
            strafeRight = true;
            Debug.Log("Collided with Left Boundary!");
        }
        if (collision.gameObject.name == "RightBoundary") {
            strafeLeft = true;
            strafeRight = false;
            Debug.Log("Collided with Right Boundary!");
        }
    }

    void StrafeChance(){
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
    
    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Attack zone entered.");
        if (collision.gameObject.name == "EnemyAttackZone") {
           move = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.name == "EnemyAttackZone") {
            StrafeChance();
        }
        if (collision.gameObject.name == "EnemyDespawnZone"){
            if (objectPool == null){
                objectPool.Release(this);
            }
            objectPool.Release(this);
            isAlive = false;
            ResetEnemy();
        }
    }

    void initiateMovement(){
        if(move) {
            transform.position += new Vector3(0, -enemyMovementSpeed * Time.deltaTime, 0);
        }
        if(strafeLeft) {
            transform.position += new Vector3(-enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
        if(strafeRight) {
            transform.position += new Vector3(enemyMovementSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void ResetEnemy()
    {
        transform.position = enemySpawner.transform.position;
        isAlive = true;
        move = false;
        strafeLeft = false;
        strafeRight = false;
    }
}
