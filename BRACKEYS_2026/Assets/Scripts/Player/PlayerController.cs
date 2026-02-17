using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    private float MoveSpeed => playerInfo.moveSpeed;

    private PlayerInfoSO playerInfo;
    public PlayerInfoSO PlayerInfo { set { playerInfo = value; } }

    private Rigidbody2D rb; 
    private PlayerInputActions playerActions;
    private InputAction move, dash, wait;

    private Vector2 currentInput = Vector2.zero;

    private void Awake() {
        playerActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        playerActions.Enable();

        move = playerActions.Player.Move;
        move.Enable();
        move.performed += OnMove;
        move.canceled += OnCancelMove;

    }
    private void OnDisable() {
        move.performed -= OnMove;
        move.canceled -= OnCancelMove;

        playerActions.Disable();
    }
    private void FixedUpdate() {
        Move();
    }
    private void OnMove(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>();
    }
    private void OnCancelMove(InputAction.CallbackContext context) {
        currentInput = Vector2.zero; 
    }

    private void Move() {
        Vector2 targetPos = (Vector2)transform.position + (currentInput.normalized * 100f);
        transform.position = Vector2.MoveTowards(transform.position, Vector2.Lerp(transform.position, targetPos, MoveSpeed), MoveSpeed);

    }



}
