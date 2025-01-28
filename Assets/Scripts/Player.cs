using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D RB;

    [Header("Movement")]
    [SerializeField] float moveSpeedX; 
    [SerializeField] float moveSpeedY;

    float inputHorizontal, inputVertical;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        inputHorizontal = UserInput.Instance.MoveInput.x;
        inputVertical = UserInput.Instance.MoveInput.y;
    }

    void FixedUpdate() {
        CheckPhysics();
    }

    void CheckPhysics() {
        RB.velocity = new Vector2(inputHorizontal * moveSpeedX, inputVertical * moveSpeedY);   
    }
}

