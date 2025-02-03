using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    public static PlayerInput PlayerInput; 

    public Vector2 MoveInput { get; private set; }
    public bool FireInput { get; private set; }

    InputAction moveAction, fireAction;

    void Awake() {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        PlayerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }

    void Update() {
        UpdateInputActions();
    }

    // Set up the inputs
    void SetupInputActions() {
        moveAction = PlayerInput.actions["Move"];
        fireAction = PlayerInput.actions["Fire"];
    }

    void UpdateInputActions() {
        MoveInput = moveAction.ReadValue<Vector2>();
        FireInput = fireAction.WasPressedThisFrame();
    }
}
