using UnityEngine;

public class KirbyGroundEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    [SerializeField] Transform groundCheckPoint;
    private bool isGrounded = true;

    private bool isJumping = false;

    protected override void Attack()
    {
    }

    protected override void Move()
    {
        // check if grounded
        GroundCheck();
        if (isGrounded)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(direction * speed, 0);
        }
        else
        {
            rb.gravityScale = gravity;
        }

        // move across screen

        // leap when trigger collider activates
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
        if (reporter.name == "JumpCollider_Left")
        {
            Jump();
        }

        if (reporter.name == "JumpCollider_Right")
        {
            Jump();
        }

    }

    public void Jump()
    {
        Debug.Log("trying to jump");
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        }
    }

}

