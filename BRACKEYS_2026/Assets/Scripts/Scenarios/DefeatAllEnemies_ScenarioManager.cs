using System;
using UnityEngine;

public class DefeatAllEnemies_ScenarioManager : ScenarioManager {
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int numberOfEnemies;
    private int destroyedEnemies;

    public override void SetupGame() {
        base.SetupGame(); 
        for(int i = 0; i < numberOfEnemies; i++) {
            GameObject g = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            Enemy e = g.GetComponent<Enemy>();
            e.destroyed.Subscribe(OnEnemyDestroyed);
        }
    }
    protected void OnEnemyDestroyed() {
        destroyedEnemies++;
        if (destroyedEnemies >= numberOfEnemies) { 
            OnWinCon(); 
        }
    }
    protected override void OnWinCon() {
        Debug.Log("YOU CAN DEW EET");
    }

}
