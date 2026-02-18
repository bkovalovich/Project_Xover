using System;
using UnityEngine;

public class ScoreAPoint_ScenarioManager : ScenarioManager
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject enemyGoalPrefab;
    [SerializeField] GameObject playerGoalPrefab;
    public override void SetupGame()
    {
        GameManager.instance.player.transform.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.gameObject.CompareTag("PlayerGoal"))
            {
                OnWinCon();
            }
        }
    }

    public void CollisionDetected(Boolean winCon)
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
        Debug.Log("You Scored a Point");
    }

    private void OnLoseCon()
    {
        Debug.Log("Enemy Scored a Point");
    }
}
