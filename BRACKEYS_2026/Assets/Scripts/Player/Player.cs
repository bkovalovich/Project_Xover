using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerInfoSO playerInfo;
    [SerializeField] public WarpTrajectory warpTrajectory;

    //STATEMACHINE
    public PlayerStateMachine stateMachine = new PlayerStateMachine();
    private Move_PlayerState moveState;
    private Dash_PlayerState dashState;
    private Warp_PlayerState warpState;
    private Knockback_PlayerState knockbackState;

    private Coroutine attackCoroutine;
    private Animator animator;

    //COMPONENTS
    [SerializeField] Transform rotationParent;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sr;
    [SerializeField] private ParticleSystem deathParticles;
    private PlayerAttack attackScript;

    //INPUT
    private PlayerInputActions playerActions;
    private InputAction move, dash, attack, look, warp;

    [HideInInspector] public Vector2 pointOfLastCollision;
    private float currentSpeed;
    [HideInInspector] public bool holdingWarp = false, isAttacking, invulnerable;

    [HideInInspector] public Vector2 currentMoveInput, currentMouseInput, currentMouseWorldInput;

    //SOUND
    [SerializeField] public AudioSource swordSlash, teleportSFX, deadSFX;

    [HideInInspector] public bool isDead;

    private void Awake() {
        moveState = GetComponent<Move_PlayerState>(); 
        dashState = GetComponent<Dash_PlayerState>();
        warpState = GetComponent<Warp_PlayerState>();
        knockbackState = GetComponent<Knockback_PlayerState>();


        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        attackScript = GetComponentInChildren<PlayerAttack>();
        attackScript.Attack(false);

    }

    private void OnEnable() {

        isDead = false;
        playerActions = new PlayerInputActions();
        stateMachine.Initialize(moveState);
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

        warp = playerActions.Player.Warp;
        warp.Enable();
        warp.performed += OnWarp;
        warp.canceled += OnWarpCancel;
    }

    private void OnDisable() {
        if (playerActions == null) return;

        if (move != null)
        {
            move.performed -= OnMove;
            move.canceled -= OnCancelMove;
        }

        if (attack != null)
            attack.performed -= OnAttack;

        if (look != null)
            look.performed -= OnLook;

        if (dash != null)
        {
            dash.performed -= OnDash;
            dash.canceled -= OnDashCancel;
        }

        if (warp != null)
        {
            warp.performed -= OnWarp;
            warp.canceled -= OnWarpCancel;
        }

        playerActions?.Disable();
        playerActions.Dispose();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 7 && stateMachine.IsInState(knockbackState) == false && !invulnerable) {
            pointOfLastCollision = collision.gameObject.transform.position;
            stateMachine.ChangeState(knockbackState);
            GameManager.instance.PlayerTakeDamage();
            StartCoroutine(Invulnerability(1.7f));
        }
    }
    //CALLBACKS
    private void OnMove(InputAction.CallbackContext context) {
        currentMoveInput = context.ReadValue<Vector2>(); 
    }
    private void OnCancelMove(InputAction.CallbackContext context) {
        currentMoveInput = Vector2.zero;
    }
    private void OnAttack(InputAction.CallbackContext context) {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(Attack(playerInfo.attackLength));
    }
    private void OnLook(InputAction.CallbackContext context) {
        currentMouseInput = context.ReadValue<Vector2>();
        currentMouseWorldInput = GameManager.instance.currentCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    private void OnDash(InputAction.CallbackContext context) {
        stateMachine.ChangeState(dashState); 
    }
    private void OnDashCancel(InputAction.CallbackContext context) {
        stateMachine.ChangeToDefault(); 
    }
    private void OnWarp(InputAction.CallbackContext context) {
        holdingWarp = true;
        stateMachine.ChangeState(warpState);
    }
    private void OnWarpCancel(InputAction.CallbackContext context) {
        holdingWarp = false;
    }

    private void RotateToMouse() {
        Vector3 direction = (currentMouseWorldInput - (Vector2)rotationParent.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotationParent.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }
    private void Update() {
        if (isDead) return; 

        RotateToMouse();
        Animate(); 
        stateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate() {
        if (isDead) return;

        stateMachine.CurrentState.PhysicsUpdate();
    }
    private void Animate() {
        Vector2 input = (currentMouseWorldInput - (Vector2)transform.position).normalized;
        // Debug.Log(input);
        sr.flipX = input.x < 0 ? true : false;
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);
        animator.SetBool("Attacking", isAttacking);
    }

    IEnumerator Attack(float duration) {
        swordSlash?.PlayOneShot(swordSlash.clip); 
        isAttacking = false;
        attackScript.Attack(false);
        yield return null;
        isAttacking = true; 
        attackScript.Attack(true);
        yield return new WaitForSeconds(duration);
        attackScript.Attack(false);
        isAttacking = false; 
    }
    public IEnumerator Invulnerability(float t) {
        invulnerable = true;
        sr.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(t);
        invulnerable = false;
        sr.color = Color.white; 
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        Debug.Log("I died");
        isDead = true;
        deadSFX.Play();
        // Activate death particles
        // Play death animation
        // Disable player controls
        deathParticles.Play();
        sr.enabled = false;
        rb.linearVelocity = Vector2.zero;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
