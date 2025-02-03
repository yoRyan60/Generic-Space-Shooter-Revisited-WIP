using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D RB;
    [SerializeField] private PlayerBulletSpawner playerBulletSpawner;
    [SerializeField] float moveSpeedX; 
    [SerializeField] float moveSpeedY;
    float inputHorizontal, inputVertical;
    bool inputSpace;

    public IObjectPool<PlayerBullet> objectPool;
    public IObjectPool<PlayerBullet> ObjectPool { set => objectPool = value; }

    public bool spacebarPressed = false;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move(){
        inputHorizontal = UserInput.Instance.MoveInput.x;
        inputVertical = UserInput.Instance.MoveInput.y;
    }

    void Attack(){
        if (UserInput.Instance.FireInput && !playerBulletSpawner.hasFired){
            playerBulletSpawner?.Shoot();
        }
    }

    void FixedUpdate() {
        CheckPhysics();
    }

    void CheckPhysics() {
        RB.velocity = new Vector2(inputHorizontal * moveSpeedX, inputVertical * moveSpeedY);   
    }
}

