using System.Collections;
using UnityEngine;

public class ProtectObjestive_ScenarioManager : Enemies_ScenarioManager
{

    [SerializeField] GameObject defensiveObjectPrefab;
    [SerializeField] float defendTime;

    public override void SetupGame()  {
        base.SetupGame();
    }



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

    protected override void OnWinCon() {
        base.OnWinCon(); 
        Debug.Log("Countdown Finished");
    }
}
