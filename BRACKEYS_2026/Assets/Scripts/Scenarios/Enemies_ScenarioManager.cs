using UnityEngine;

public class Enemies_ScenarioManager : ScenarioManager
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public int numberOfEnemies;
    [SerializeField] Transform[] spawnPoints; 

    public override void SetupGame() {
        base.SetupGame();
        for (int i = 0; i < numberOfEnemies; i++) {
            SpawnEnemy();
        }
    }
    public virtual Enemy SpawnEnemy() {
        Transform spawnPoint = RandomSpawnPoints();
        GameObject gameObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        gameObject.transform.parent = this.transform;
        Enemy e = gameObject.GetComponent<Enemy>();
        if (spawnPoint.localScale.x < 0) {
            e.transform.localScale = new Vector2(e.transform.localScale.x * -1, e.transform.localScale.y);
            e.direction = -1;
        } else {
            e.direction = 1;
        }

        e.scenarioManager = this;
        return e;
    }
    private Transform RandomSpawnPoints() {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
    }
}
