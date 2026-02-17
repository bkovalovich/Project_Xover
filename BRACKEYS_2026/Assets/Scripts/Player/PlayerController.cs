using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    private float currentSpeed;
    private float MoveSpeed => playerInfo.moveSpeed;

    private PlayerInfoSO playerInfo;
    public PlayerInfoSO PlayerInfo { set { playerInfo = value; } }

    private Rigidbody2D rb; 
    private PlayerInputActions playerActions;
    private InputAction move, dash, attack, look;
    private Coroutine decreasingCoroutine, dashCoroutine, attackCoroutine; 

    [SerializeField] Transform rotationParent;
    private PlayerAttack attackScript; 
    float angleTolerance = 1f;
    private bool isDashing = false; 

    private Vector2 currentMoveInput = Vector2.zero, currentMouseInput = Vector2.zero;
    //INITS
    private void Awake() {
        playerActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        attackScript = rotationParent?.GetComponentInChildren<PlayerAttack>();
        attackScript.Attack(false);

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

        look = playerActions.Player.Look;
        look.Enable();
        look.performed += OnLook;

        dash = playerActions.Player.Dash;
        dash.Enable();
        dash.performed += OnDash;
        dash.canceled += OnDashCancel;
    }


    private void OnDisable() {
        move.performed -= OnMove;
        move.canceled -= OnCancelMove;
        attack.performed -= OnAttack;
        look.performed -= OnLook;
        dash.performed -= OnDash;
        dash.canceled -= OnDashCancel;

        playerActions.Disable();
    }
    //UPDATES
    private void FixedUpdate() {
        if(!isDashing) Move();
    }
    private void Update() {
        RotateToMouse();
    }
    //CALLBACKS
    private void OnMove(InputAction.CallbackContext context) {
        currentMoveInput = context.ReadValue<Vector2>();

        if (isDashing) return; 
        if(decreasingCoroutine != null) StopCoroutine(decreasingCoroutine);
        currentSpeed = MoveSpeed;
    }
    private void OnCancelMove(InputAction.CallbackContext context) {
        currentMoveInput = Vector2.zero;
        if (isDashing) return;
        if (decreasingCoroutine != null) StopCoroutine(decreasingCoroutine);
        decreasingCoroutine = StartCoroutine(DecreaseSpeed());
    }    
    private void OnAttack(InputAction.CallbackContext context) {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(Attack(playerInfo.attackLength));
    }
    private void OnLook(InputAction.CallbackContext context) {
        currentMouseInput = context.ReadValue<Vector2>(); 
    }
    private void OnDash(InputAction.CallbackContext context) {
        isDashing = true;
        if (dashCoroutine != null) StopCoroutine(dashCoroutine);
        dashCoroutine = StartCoroutine(Dash());
    }
    private void OnDashCancel(InputAction.CallbackContext context) {
        FinishDash();
    }
    //MOVEMENT LOGIC
    IEnumerator DecreaseSpeed() {
        float duration = 0.1f, current = 0; 
        while(current < duration) {
            currentSpeed = Mathf.Lerp(MoveSpeed, 0, current/duration);
            current += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime); 
        }
    }
    IEnumerator Dash() {
        float time = 0, duration = playerInfo.dashLength;
         Vector2 input = currentMoveInput == Vector2.zero ? Vector2.up : currentMoveInput;
        input = input.normalized; 

        currentSpeed = playerInfo.dashSpeed;
        rb.linearVelocity = input * new Vector2(currentSpeed, currentSpeed) * Time.deltaTime;
        //rb.linearVelocity = Vector2.zero; 
        while (time < duration && isDashing) {
            time += Time.deltaTime; 
            yield return new WaitForSeconds(Time.deltaTime);
        }
        FinishDash(); 
    }
    IEnumerator Attack(float duration) {
        attackScript.Attack(true);
        yield return new WaitForSeconds(duration);
        attackScript.Attack(false);
    }
    private void FinishDash() {
        currentSpeed = playerInfo.moveSpeed;
        isDashing = false; 
    }
    private void Move() {
       // Vector2 input = currentMoveInput == Vector2.zero ? lastMoveInput : currentMoveInput; 
        rb.linearVelocity = currentMoveInput.normalized * new Vector2(currentSpeed, currentSpeed) * Time.deltaTime;
    }
    private void RotateToMouse() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(currentMouseInput);
        mouseWorldPos.z = 0f;

        Vector3 direction = mouseWorldPos - rotationParent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotationParent.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }




}
