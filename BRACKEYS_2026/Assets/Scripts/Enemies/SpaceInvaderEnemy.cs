using UnityEngine;

public class SpaceInvaderEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    protected override void Attack()
    {
    }

    protected override void Move()
    {
        rb.position = Vector2.MoveTowards(rb.position, target.transform.position, speed);
    }
}
