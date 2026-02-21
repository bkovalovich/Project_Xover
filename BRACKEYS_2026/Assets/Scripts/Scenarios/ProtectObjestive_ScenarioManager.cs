using System.Collections;
using UnityEngine;

public class ProtectObjestive_ScenarioManager : Enemies_ScenarioManager
{
    [SerializeField] GameObject defensiveObjectPrefab;
    [SerializeField] float defendTime;
    public override void SetupGame()  {
        base.SetupGame();
        scenarioCanvas.transform.Find("Timer").gameObject.SetActive(true);
/*      Canvas spaceInvadersCanvas = transform.Find("UI").GetComponent<Canvas>();
        spaceInvadersCanvas.worldCamera = scenarioCamera;*/
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
        StartCoroutine(RespawnTime());
    }
    protected override void OnWinCon() {
        base.OnWinCon(); 
        Debug.Log("Countdown Finished");
    }
    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(Random.Range(1f,5f));
        SpawnEnemy();
    }
}
