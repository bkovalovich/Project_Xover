using System;
using UnityEngine;

public class ScoreAPoint_ScenarioManager : ScenarioManager
{
    [SerializeField] GameObject pointScoringPrefab;
    [SerializeField] GameObject enemyGoalPrefab;
    [SerializeField] GameObject playerGoalPrefab;
    public override void SetupGame()
    {
        base.SetupGame();
        GameManager.instance.player.transform.position = transform.position;
    }

    public void CollisionDetected(bool winCon)
    {
        if (winCon)
        {
            OnWinCon();
        }
        else
        {
            OnLoseCon();
        }
    }
    protected override void OnWinCon()
    {
        base.OnWinCon();
        Debug.Log("You Scored a Point");
    }

    protected override void OnLoseCon() {
        base.OnLoseCon();
        Debug.Log("Enemy Scored a Point");
    }
}
