using UnityEngine;

public class KirbyGroundEnemy : Enemy
{
    [SerializeField] float speed;
    //[SerializeField] float jumpForce;
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

            rb.linearVelocity = new Vector2(direction * speed, 0);
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
        if (isJumping) return; // prevent multiple jumps while already in the air

        float colliderTopY = other.bounds.max.y;
        float jumpHeight = (colliderTopY - transform.position.y) + 0.1f; // add a small buffer to ensure the jump is high enough
        float grav = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float jumpVelocity = Mathf.Sqrt(2 * grav * jumpHeight);


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
            rb.AddForce(new Vector2(0, jumpVelocity), ForceMode2D.Impulse);

        }
    }

}

