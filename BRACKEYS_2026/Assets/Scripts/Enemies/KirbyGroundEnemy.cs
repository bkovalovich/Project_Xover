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

    protected override void Attack()
    {
    }

    protected override void Move()
    {
        // check if grounded
        GroundCheck();
        if (isGrounded)
        {
            WanderTimerCheck();
            if (Wander)
                rb.linearVelocity = new Vector2(direction * speed, 0);
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
            int temp = Random.Range(0, 2);
            if (temp == 0)
            {
                direction = -1;
                sprite.flipX = true;
            }

            else
            {
                direction = 1;
                sprite.flipX = false;
            }
        }
    }

    public void GroundCheck()
    {
        // check if grounded and set isGrounded accordingly
        if ( rb.linearVelocityY > 0)
        {
            isGrounded = false;
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, LayerMask.GetMask("Ground"));
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    public void OnChildTriggerEnter2D(Collider2D other, TriggerReporter reporter)
    {
        if (isJumping) return; // prevent multiple jumps while already in the air

        float colliderTopY = other.bounds.max.y;
        float jumpHeight = (colliderTopY - transform.position.y) + 0.1f; // add a small buffer to ensure the jump is high enough
        float grav = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float jumpVelocity = Mathf.Sqrt(2 * grav * jumpHeight);

        float playerX = other.gameObject.transform.position.x;
        if (playerX > transform.position.x)
            direction = 1;
        else
            direction = -1;


        if (reporter.name == "JumpCollider_Left")
        {
            Jump(jumpVelocity);
        }

        if (reporter.name == "JumpCollider_Right")
        {
            Jump(jumpVelocity);
        }

    }

    public void Jump(float jumpVelocity)
    {
        Debug.Log("trying to jump");
        if (isGrounded)
        {

            isJumping = true;
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(direction * speed, jumpVelocity), ForceMode2D.Impulse);

        }
    }

}

