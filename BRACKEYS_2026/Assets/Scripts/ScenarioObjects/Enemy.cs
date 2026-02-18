using UnityEngine;

public abstract class Enemy : ScenarioObject {
    protected int health;

    public Event destroyed = new Event(); 

    protected abstract void Attack();
    protected abstract void Move();

    protected void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "PlayerAttack") {
            TakeDamage(); 
        }
    }
    protected virtual void TakeDamage() {
        health--;
        if (health <= 0) OnDead();
    }
    protected virtual void OnDead() {
        destroyed.Trigger();
        Destroy(this.gameObject);
    }
    protected void Update() {
        Move();
    }
}
