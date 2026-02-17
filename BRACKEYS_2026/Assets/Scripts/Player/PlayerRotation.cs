using UnityEngine;
using System.Collections; 

public class PlayerRotation : MonoBehaviour
{
    private CapsuleCollider2D hitbox;

    private void Awake() {
        hitbox = GetComponentInChildren<CapsuleCollider2D>(); 
    }

    private void OnEnable() {
        hitbox.enabled = false; 
    }

    public void OnAttack(float duration) {
        StopAllCoroutines();
        StartCoroutine(Attack(duration));
    }

    IEnumerator Attack(float duration) {
        hitbox.enabled = true;
        yield return new WaitForSeconds(duration);
        hitbox.enabled = false; 
    }

}
