using UnityEngine;

public class DefensiveObject : ScenarioObject
{
    [SerializeField] protected int health;

    public Event destroyed = new Event();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    protected virtual void TakeDamage()
    {
        health--;
        if (health <= 0) OnDestroyed();
    }

    protected virtual void OnDestroyed()
    {
        destroyed.Trigger();
        Destroy(this.gameObject);
    }
}
