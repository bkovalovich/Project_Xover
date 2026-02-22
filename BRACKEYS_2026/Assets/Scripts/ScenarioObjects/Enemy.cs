using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Enemy : ScenarioObject {
    [SerializeField] GameObject oneShotDeathSFX; 
    [HideInInspector] public int direction;

    protected Rigidbody2D rb; 
    [HideInInspector] public Enemies_ScenarioManager scenarioManager; 
    [SerializeField] public int health = 3;
    [SerializeField] GameObject particleBurst; 

    public Event destroyed = new Event(); 

    protected abstract void Attack();
    protected abstract void Move();
    private AudioSource swordHit; 

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        swordHit = GetComponent<AudioSource>();
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerAttack") {
            TakeDamage();
            swordHit.Play(); 
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
        if(oneShotDeathSFX) Instantiate(oneShotDeathSFX);
        if(particleBurst) Instantiate(particleBurst, transform.position, Quaternion.identity);
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
