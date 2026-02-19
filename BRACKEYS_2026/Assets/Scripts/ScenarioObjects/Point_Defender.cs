using System.Collections;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float thinkingTime;
    public Transform ball;
    private bool waiting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!waiting)
        {
            Movement();
        }   
    }

    private void Movement()
    {
        if (Random.Range(0f, 2f) > 0.1f)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, ball.position.y, moveSpeed * Time.deltaTime));
        }
        else
        {
            StartCoroutine(Wait(thinkingTime));
        }
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        waiting = false;
    }
}
