using System.Collections;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float thinkingTime;
    public Transform ball;
    private bool waiting = false;
    private float prevBallX;
    private bool ballIsComing, lastFrameBallIsComing;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastFrameBallIsComing = true;
    }

    private void Update()
    {
        CheckIfBallIsComing();
        if (ballIsComing && !lastFrameBallIsComing)
        {
            StartCoroutine(Wait(thinkingTime));
            thinkingTime = thinkingTime * 1.1f;
        }

    }

    private void LateUpdate()
    {
        prevBallX = ball.position.x;
        lastFrameBallIsComing = ballIsComing;
    }

    private void FixedUpdate()
    {
        if (!waiting)
        {
            Movement();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // when ball is traveling towards paddle (pos X) move towards ball Y position
    // how to make AI imperfect?
    // increase ball speed over time
    // make AI intentionally mess up via randomness or momentum
    private void Movement()
    {
        float distanceY = ball.position.y - rb.position.y;
        // Proportional control: speed scales with distance, but clamp to max speed
        float velocityY = Mathf.Clamp(distanceY * moveSpeed, -moveSpeed, moveSpeed);
        rb.linearVelocity = new Vector2(0, velocityY);
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        waiting = false;
    }

    private void CheckIfBallIsComing()
    {
        if (prevBallX < ball.position.x)
        {
            ballIsComing = true;
        }
        else
        {
            ballIsComing = false;
        }
    }
}
