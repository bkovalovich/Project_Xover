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
    }

    private void Update()
    {
        CheckIfBallIsComing();
        if (ballIsComing && !lastFrameBallIsComing)
        {
            StartCoroutine(Wait(thinkingTime));
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
    }

    // when ball is traveling towards paddle (pos X) move towards ball Y position
    // how to make AI imperfect?
    // increase ball speed over time
    // make AI intentionally mess up via randomness or momentum
    private void Movement()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, ball.position.y, moveSpeed * Time.deltaTime));
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
