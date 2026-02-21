using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}

public class Kirby_Scenario : DefeatAllEnemies_ScenarioManager
{
    public EnemySpawnData[] enemySpawnDataList;
    private Queue<EnemySpawnData> last2EnemiesSpawned = new Queue<EnemySpawnData>(2);

    public override Enemy SpawnEnemy()
    {
        EnemySpawnData enemy = RandomEnemy();
        GameObject gameObject = Instantiate(enemy.enemyPrefab, enemy.spawnPoint.position, Quaternion.identity);
        gameObject.transform.parent = this.transform;
        Enemy e = gameObject.GetComponent<Enemy>();
        if (enemy.spawnPoint.localScale.x < 0)
        {
            e.transform.localScale = new Vector2(e.transform.localScale.x * -1, e.transform.localScale.y);
            e.direction = -1;
        }
        else
        {
            e.direction = 1;
        }

        e.scenarioManager = this;
        return e;
    }

    private EnemySpawnData RandomEnemy()
    {
        EnemySpawnData newEnemy;
        int attempts = 0;

        do
        {
            newEnemy = enemySpawnDataList[Random.Range(0, enemySpawnDataList.Length)];
            attempts++;
        }
        while (last2EnemiesSpawned.Contains(newEnemy) && attempts < 10);

        // Add newest
        last2EnemiesSpawned.Enqueue(newEnemy);

        // Remove oldest if more than 2 stored
        if (last2EnemiesSpawned.Count > 2)
            last2EnemiesSpawned.Dequeue();

        return newEnemy;
    }
}
