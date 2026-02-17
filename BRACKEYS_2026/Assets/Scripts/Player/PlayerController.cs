using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float currentSpeed;
    private float MoveSpeed => playerInfo.moveSpeed;

    private PlayerInfoSO playerInfo;
    public PlayerInfoSO PlayerInfo { set { playerInfo = value; } }

    private Rigidbody2D rb; 
    private PlayerInputActions playerActions;
    private InputAction move, dash, attack, look;

    private PlayerRotation playerRotation; 

    private Vector2 currentInput = Vector2.zero, lastInput = Vector2.zero;

    private void Awake() {
        playerActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        playerRotation = GetComponentInChildren<PlayerRotation>(); 
    }
    private void OnEnable() {
        playerActions.Enable();

        move = playerActions.Player.Move;
        move.Enable();
        move.performed += OnMove;
        move.canceled += OnCancelMove;

        attack = playerActions.Player.Attack;
        attack.Enable();
        attack.performed += OnAttack; 
    }
    private void OnDisable() {
        move.performed -= OnMove;
        move.canceled -= OnCancelMove;

        attack.performed -= OnAttack; 

        playerActions.Disable();
    }
    private void FixedUpdate() {
        Move();
    }
    private void OnMove(InputAction.CallbackContext context) {
        StopAllCoroutines();
        currentSpeed = MoveSpeed;
        currentInput = context.ReadValue<Vector2>();
    }
    private void OnCancelMove(InputAction.CallbackContext context) {    Debug.Log("buh");
        lastInput = currentInput; 
        currentInput = Vector2.zero;
        StartCoroutine(DecreaseSpeed());
    }    
    private void OnAttack(InputAction.CallbackContext context) {
        playerRotation.OnAttack(playerInfo.attackLength); 
    }
    IEnumerator DecreaseSpeed() {
        float duration = 0.1f, current = 0; 
        while(current < duration) {
            currentSpeed = Mathf.Lerp(MoveSpeed, 0, current/duration);
            current += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime); 
        }
    }

    private void Move() {
        Vector2 input = currentInput == Vector2.zero ? lastInput : currentInput; 
        rb.linearVelocity = input * new Vector2(currentSpeed, currentSpeed) * Time.deltaTime;
    }



}
