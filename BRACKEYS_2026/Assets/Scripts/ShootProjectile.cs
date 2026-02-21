using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fireRate; 
    [SerializeField] bool looping;
    private float timer;

    private void Awake() {
        timer = fireRate;     
    }

    public void Shoot() {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    private void FixedUpdate() {
        if (!looping) return;
        timer += Time.deltaTime;
        if(timer > fireRate) {
            timer = 0;
            Shoot();
        }
    }
}
