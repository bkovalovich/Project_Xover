using UnityEngine;

public class KirbyFlyingEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float amplitude;
    [SerializeField] float frequence;
    private float startY;
    private float timeElapsed;

    protected new void Awake()
    {
        base.Awake();
        startY = transform.position.y + Random.Range(-1, 1);
        timeElapsed = 0f;
    }

    protected override void Attack()
    {

    }

    protected override void Move()
    {
        timeElapsed += Time.deltaTime;
        float newY = startY + amplitude * Mathf.Sin(frequence * timeElapsed);
        float newX = transform.position.x + direction * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(newX, newY));
    }
}
