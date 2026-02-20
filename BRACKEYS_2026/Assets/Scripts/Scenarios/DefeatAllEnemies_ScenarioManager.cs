using System;
using UnityEngine;

public class DefeatAllEnemies_ScenarioManager : Enemies_ScenarioManager {

    private int destroyedEnemies;

    public override void SetupGame() {
        base.SetupGame();
        destroyedEnemies = 0;
        UpdateObjText(); 
    }

    protected void OnEnemyDestroyed() {
        Debug.Log("enemy destroyed"); 
        destroyedEnemies++;
        if (destroyedEnemies >= numberOfEnemies) {
            OnWinCon(); 
        }
        UpdateObjText(); 
    }
    public override Enemy SpawnEnemy() {
        Enemy e = base.SpawnEnemy();
        e.destroyed.Subscribe(OnEnemyDestroyed);
        return e; 
    }
    private void UpdateObjText() {
       objectiveText.text = $"Defeat {destroyedEnemies}/{numberOfEnemies} enemies";
    }

}
