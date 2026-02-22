using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float lifeSpan, speed;
    private Rigidbody2D rb;
    private float timer = 0; 

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed; 
    }
    private void Update() {
        timer += Time.deltaTime; 
        if(timer > lifeSpan) {
            OnTimerUp();
        }
    }
    protected virtual void OnTimerUp() {
        Destroy(gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "DeathPlane" || collision.gameObject.tag == "PlayerAttack") {
            Destroy(this.gameObject);
        }

    }
}
