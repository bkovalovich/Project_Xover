using System.Collections;
using UnityEngine;

public class ProtectObjestive_ScenarioManager : ScenarioManager
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int numberOfEnemies;
    [SerializeField] GameObject defensiveObjectPrefab;
    [SerializeField] float defendTime;

    public override void SetupGame()
    {
        base.SetupGame();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject g = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            Enemy e = g.GetComponent<Enemy>();
            e.destroyed.Subscribe(OnEnemyDestroyed);
        }
        GameManager.instance.player.transform.position = transform.position;
    }

    //DELETE AFTER GAMEMANAGER IS FUNCTIONAL VV
    public void Awake()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject g = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            Enemy e = g.GetComponent<Enemy>();
            e.destroyed.Subscribe(OnEnemyDestroyed);
        }
    }
    //DELETE AFTER GAMEMANAGER IS FUNCTIONAL ^^

    public float GetDefendTime()
    {
        return (defendTime);
    }

    public void VictoryCheck(bool win)
    {
        if (win && defensiveObjectPrefab != null)
        {
            OnWinCon();
        }
    }

    protected void OnEnemyDestroyed()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWinCon()
    {
        Debug.Log("Countdown Finished");
    }
}
