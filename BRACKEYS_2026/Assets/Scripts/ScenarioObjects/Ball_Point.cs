using System;
using UnityEngine;
using System.Collections;

public class Ball : ScenarioObject, IScenarioListener
{
    [SerializeField] float startingSpeed = 6f;
    [SerializeField] float reboundSpeedMultiplier = 1.1f;
    [SerializeField] float maxSpeed = 10f;

    private float minSpeed;
    private Rigidbody2D rb;
    private Collider2D col;
    private float currentSpeed;
    private Vector2 startingPos;

    public void OnScenarioStateChanged(bool isActive)
    {
        minSpeed = startingSpeed * 0.5f;
        currentSpeed = isActive ? startingSpeed : minSpeed;

        rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        startingPos = rb.position;
        LaunchBall();
    }

    private void FixedUpdate()
    {
        // Only clamp speed — DO NOT change direction
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // Prevent perfectly vertical motion without killing physics
        if (Mathf.Abs(rb.linearVelocity.x) < 0.25f)
        {
            float sign = Mathf.Sign(rb.linearVelocity.x);
            if (sign == 0) sign = UnityEngine.Random.value > 0.5f ? 1 : -1;

            rb.linearVelocity += new Vector2(sign * 0.3f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Boundary Left":
                transform.parent
                    .GetComponent<ScoreAPoint_ScenarioManager>()
                    .CollisionDetected(false);
                Destroy(gameObject);
                return;

            case "Boundary Right":
                transform.parent
                    .GetComponent<ScoreAPoint_ScenarioManager>()
                    .CollisionDetected(true);
                Destroy(gameObject);
                return;
        }

        if (collision.gameObject.CompareTag("DeathPlane"))
            StartCoroutine(Reset());
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
        col.enabled = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForFixedUpdate();

        rb.position = startingPos;

        yield return new WaitForSeconds(2f);

        col.enabled = true;
        LaunchBall();
    }

    void LaunchBall()
    {
        Vector2 dir = new Vector2(
            UnityEngine.Random.value > 0.5f ? 1 : .5f,
            UnityEngine.Random.Range(-0.7f, 0.7f)
        ).normalized;

        rb.linearVelocity = dir * startingSpeed;
    }
}