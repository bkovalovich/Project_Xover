using System;
using UnityEngine;

public class ScoreAPoint_ScenarioManager : ScenarioManager
{
    public override void SetupGame()
    {
        base.SetupGame();
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
        string id = GetComponentInParent<ScenarioContainer>().ID;
        Debug.Log("You Scored a Point" + " in ID: " + id);
    }

    protected override void OnLoseCon() {
        base.OnLoseCon();
        string id = GetComponentInParent<ScenarioContainer>().ID;
        Debug.Log("Enemy Scored a Point in ID: " + id);
    }
}
