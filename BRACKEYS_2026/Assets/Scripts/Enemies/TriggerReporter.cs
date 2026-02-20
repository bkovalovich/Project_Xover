using UnityEngine;

public class TriggerReporter : MonoBehaviour
{
    public KirbyGroundEnemy enemy;

    private void Awake()
    {
        if (enemy == null)
            enemy = GetComponentInParent<KirbyGroundEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            enemy.OnChildTriggerEnter2D(other, this);
        }
    }
}
