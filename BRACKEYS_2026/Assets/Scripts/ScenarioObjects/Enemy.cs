using UnityEngine;

public abstract class Enemy : ScenarioObject {
    protected int health;

    protected abstract void Attack();
    protected abstract void Move();

    protected void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "PlayerAttack") {
            TakeDamage(); 
        }
    }
    protected virtual void TakeDamage() {
        Debug.Log("took damage");
        health--;
    }
    protected virtual void OnDead() {
        if (isWinCon) winConCompleted.Use(); 
    }
    protected void Update() {
        Move();
    }
}
