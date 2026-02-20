using UnityEngine;

public class ScrollingEnemy : Enemy {
    [SerializeField] float speed;
    protected override void Attack() {
    }

    protected override void Move() {
        rb.linearVelocity = new Vector2(direction * speed, 0);
    }
}
