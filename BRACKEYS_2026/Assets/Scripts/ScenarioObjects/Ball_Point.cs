using System;
using UnityEngine;

public class Ball : ScenarioObject
{
    [SerializeField] float startingSpeed;
    [SerializeField] float reboundSpeedMultiplier;
    [SerializeField] float maxSpeed = 10;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(UnityEngine.Random.Range(0f, 1f) * startingSpeed, UnityEngine.Random.Range(-1f, 1f)) * startingSpeed;
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Boundry Top":
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * reboundSpeedMultiplier, -rb.linearVelocity.y * reboundSpeedMultiplier);
                break;
            case "Boundry Bottom": 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * reboundSpeedMultiplier, -rb.linearVelocity.y * reboundSpeedMultiplier);
                break;
            case "Boundry Left":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(false);
                break;
            case "Boundry Right":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(true);
                break;
        }
    }
}
