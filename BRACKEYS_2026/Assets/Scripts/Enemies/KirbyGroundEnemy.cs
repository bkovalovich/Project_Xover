using UnityEngine;

public class KirbyGroundEnemy : Enemy
{
    [SerializeField] float speed;
    //[SerializeField] float jumpForce;
    [SerializeField] Transform groundCheckPoint;
    private bool isGrounded = true;

    private bool isJumping = false;

    [SerializeField] private float wanderTimer = 4f;
    private bool Wander = true;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite walkSprite;
    [SerializeField] private Sprite jumpSprite;

    protected new void Awake()
    {
        base.Awake();
        wanderTimer = wanderTimer + Random.Range(-1, 2);
    }

    protected override void Attack()
    {
    }

    protected override void Move()
    {
        UpdateSpriteFacing();
        // check if grounded
        GroundCheck();

        if (isJumping)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocityY);
            return;
        }

        if (isGrounded)
        {
            WanderTimerCheck();
            if (Wander)
            {
                rb.linearVelocity = new Vector2(direction * speed, 0);
            }
            else
                rb.linearVelocity = new Vector2(0, 0);
        }


        // move across screen

        // leap when trigger collider activates
    }

    public void WanderTimerCheck()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
        {
            Wander = !Wander;
            wanderTimer = 2f;
            if (Wander)
            {
                WanderRandomDirection();
            }

        }
    }

    public void WanderRandomDirection()
    {
        int temp = Random.Range(0, 2);
        if (temp == 0)
        {
            direction = -1;

        }

        else
        {
            direction = 1;

        }
    }

    public void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
        if (isGrounded)
        {
            sprite.sprite = walkSprite;
            WallCheck();
        }
        else
        {
            sprite.sprite = jumpSprite;
        }

        if (isGrounded && rb.linearVelocityY <= 0)
        {
            isJumping = false;
        }
    }

    public void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            Jump(6f, direction);
        }
    }

    public void OnChildTriggerEnter2D(Collider2D other, TriggerReporter reporter)
    {
        if (isJumping) return; // prevent multiple jumps while already in the air

        float colliderTopY = other.bounds.max.y;
        float jumpHeight = (colliderTopY - transform.position.y) + 0.1f; // add a small buffer to ensure the jump is high enough
        float grav = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float jumpVelocity = Mathf.Sqrt(2 * grav * jumpHeight);


        if (reporter.name == "JumpCollider_Left")
        {
            Jump(jumpVelocity, -1);
        }

        if (reporter.name == "JumpCollider_Right")
        {
            Jump(jumpVelocity, 1);
        }

    }

    public void Jump(float jumpVelocity, int dir)
    {
        Debug.Log("trying to jump" + isGrounded + isJumping);
        if (isGrounded && !isJumping)
        {

            isJumping = true;
            rb.linearVelocity = new Vector2(0, 0);
            direction = dir;
            rb.AddForce(new Vector2(direction * speed, jumpVelocity), ForceMode2D.Impulse);

        }
    }

    public void UpdateSpriteFacing()
    {
        if (rb.linearVelocityX > 0.05)
        {
            sprite.flipX = true;
        }
        else if (rb.linearVelocityX < -0.05)
        {
            sprite.flipX = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * direction * 1f);
    }
}

