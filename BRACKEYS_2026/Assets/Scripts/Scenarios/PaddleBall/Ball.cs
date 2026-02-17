using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float startingSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(Random.Range(0f, 1f) * startingSpeed, Random.Range(-1f, 1f)) * startingSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Boundry Top":
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
                break;
            case "Boundry Bottom": 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
                break;
        }
    }
}
