using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    private AudioSource shoot; 
    [SerializeField] GameObject projectile;
    [SerializeField] float fireRate; 
    [SerializeField] bool looping;
    private float timer;

    private void Awake() {
        timer = fireRate;
        shoot = GetComponent<AudioSource>(); 
    }

    public void Shoot() {
        if(shoot != null)
            shoot?.Play(); 
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
