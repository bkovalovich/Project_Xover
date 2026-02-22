using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CapsuleCollider2D cc;
    public void Attack(bool enabled) {
        sr.enabled = enabled;
        cc.enabled = enabled; 
    }
}
