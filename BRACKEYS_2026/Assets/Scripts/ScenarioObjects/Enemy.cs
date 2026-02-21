using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Enemy : ScenarioObject {
    [HideInInspector] public int direction;

    protected Rigidbody2D rb; 
    [HideInInspector] public Enemies_ScenarioManager scenarioManager; 
    [SerializeField] public int health;

    public Event destroyed = new Event(); 

    protected abstract void Attack();
    protected abstract void Move();

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>(); 
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "PlayerAttack") {
            TakeDamage(); 
        }
        if(collision.gameObject.tag == "DeathPlane") {
            scenarioManager.SpawnEnemy();
            Destroy(this.gameObject);
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

    public virtual void SetFacing(int dir)
    {
        direction = dir < 0 ? -1 : 1;

        // flip visuals only, not transform scale
        if (TryGetComponent<SpriteRenderer>(out var sr))
            sr.flipX = direction < 0;
    }
}
