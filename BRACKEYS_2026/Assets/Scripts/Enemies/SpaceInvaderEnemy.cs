
using UnityEngine;

public class SpaceInvaderEnemy : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;
    [SerializeField] private float attackRange;

    private void Start()
    {
        Transform parent = transform.parent;
        target = parent.Find("Objective").gameObject;
        attackRange = Random.Range(2f, 5f);
    }
    protected override void Attack()
    {
        if (Vector2.Distance(rb.position, target.transform.position) <= attackRange)
        {
            throw new System.NotImplementedException();
        }
    }

    protected override void Move()
    {
        if (Vector2.Distance(rb.position, target.transform.position) >= attackRange)
        {
            rb.position = Vector2.Lerp(rb.position, target.transform.position, speed * Time.deltaTime);
        }
    }

}
