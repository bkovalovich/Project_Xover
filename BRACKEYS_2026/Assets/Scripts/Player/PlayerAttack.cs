using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpriteRenderer sr;
    private CapsuleCollider2D cc;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>(); 
    }
    public void Attack(bool enabled) {
        sr.enabled = enabled;
        cc.enabled = enabled; 
    }
}
