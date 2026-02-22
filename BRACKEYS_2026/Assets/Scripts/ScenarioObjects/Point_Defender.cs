using System.Collections;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public Transform ball;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
    }

    private void FixedUpdate()
    {
        if (ball != null)
            Movement();
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
}
