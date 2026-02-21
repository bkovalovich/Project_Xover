using System.Collections;
using UnityEngine;

public class ProtectObjestive_ScenarioManager : Enemies_ScenarioManager
{
    [SerializeField] GameObject defensiveObjectPrefab;
    public override void SetupGame()  {
        base.SetupGame();
    }
    public override void OnTimerFinish() {
        if (defensiveObjectPrefab != null)
            OnWinCon();
        else
            OnLoseCon(); 
    }

    protected void OnEnemyDestroyed()
    {
        StartCoroutine(RespawnTime());
    }
    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(Random.Range(1f,5f));
        SpawnEnemy();
    }
}
