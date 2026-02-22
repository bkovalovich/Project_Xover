using System;
using UnityEngine;
using System.Collections;

public class Ball : ScenarioObject
{
    [SerializeField] float startingSpeed;
    [SerializeField] Vector2 startingPos;
    [SerializeField] float reboundSpeedMultiplier;
    [SerializeField] float maxSpeed = 10;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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
            case "Boundary Left":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(false);
                StartCoroutine(Reset());
                break;
            case "Boundary Right":
                transform.parent.GetComponent<ScoreAPoint_ScenarioManager>().CollisionDetected(true);
                StartCoroutine(Reset());
                break;
        }

        if (collision.gameObject.CompareTag("DeathPlane"))
        {
            StartCoroutine(Reset());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("PlayerAttack"))
        {
            Vector2 direction = -collision.transform.right.normalized;
            Vector2 newVelocity = rb.linearVelocity.magnitude * direction * reboundSpeedMultiplier;
            rb.linearVelocity = newVelocity;
        }
    }

    IEnumerator Reset()
    {
        rb.position = startingPos;
        rb.linearVelocity = Vector2.zero;
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        col.enabled = true;
        rb.linearVelocity = new Vector2(UnityEngine.Random.Range(0f, 1f) * startingSpeed, UnityEngine.Random.Range(-1f, 1f)) * startingSpeed;

    }
}
