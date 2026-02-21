using UnityEngine;

public class PokemonProjectile : EnemyProjectile
{
    [SerializeField] int projCount;
    [HideInInspector] int currentProjCount; 
    protected override void OnTimerUp() {
        if (projCount >= currentProjCount) {
            currentProjCount++;
            Vector2 nextPos = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
            GameObject nextProj = Instantiate(this.gameObject, nextPos, Quaternion.identity);
            PokemonProjectile script = nextProj.GetComponent<PokemonProjectile>();
            script.currentProjCount = currentProjCount;
        }
        base.OnTimerUp();
    }
}
