using System;
using UnityEngine;

public class Ball : ScenarioObject
{
    [SerializeField] float startingSpeed;
    [SerializeField] Vector2 startingPos;
    [SerializeField] float reboundSpeedMultiplier;
    [SerializeField] float maxSpeed = 10;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = rb.position;
        rb.linearVelocity = new Vector2(UnityEngine.Random.Range(0f, 1f) * startingSpeed, UnityEngine.Random.Range(-1f, 1f)) * startingSpeed;
    }

    private void FixedUpdate()
    {
        // add functionality to speed up over time or per collision here
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // add functionality to reflect when hitting the player's attack here

        switch (collision.gameObject.name)
        {
            case "Boundary Top":
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * reboundSpeedMultiplier, -rb.linearVelocity.y * reboundSpeedMultiplier);
                break;
            case "Boundary Bottom": 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * reboundSpeedMultiplier, -rb.linearVelocity.y * reboundSpeedMultiplier);
                break;
            case "Boundary Left":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(false);
                Reset();
                break;
            case "Boundary Right":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(true);
                Reset();
                break;
        }
    }

    public void Reset()
    {
        rb.position = startingPos;
        rb.linearVelocity = new Vector2(UnityEngine.Random.Range(0f, 1f) * startingSpeed, UnityEngine.Random.Range(-1f, 1f)) * startingSpeed;
    }
}
