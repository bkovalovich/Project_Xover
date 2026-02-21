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
    public virtual Enemy SpawnEnemy()
    {
        Transform spawnPoint = RandomSpawnPoints();
        GameObject gameObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        gameObject.transform.parent = this.transform;

        Enemy e = gameObject.GetComponent<Enemy>();

        int dir = spawnPoint.localScale.x < 0 ? -1 : 1;
        e.SetFacing(dir);

        e.scenarioManager = this;
        return e;
    }
    private Transform RandomSpawnPoints() {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
    }
}
