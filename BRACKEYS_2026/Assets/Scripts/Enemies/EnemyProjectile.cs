using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float lifeSpan, speed;
    private Rigidbody2D rb;
    private float timer = 0; 

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(speed, speed));
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
}
