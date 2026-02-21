using UnityEngine;

public class DefeatCertainEnemy_ScenarioManager : Enemies_ScenarioManager {
    [SerializeField] GameObject bossEnemy;
    [SerializeField] Transform bossSpawn; 
 
    public override void SetupGame() {
        base.SetupGame();
        GameObject g = Instantiate(bossEnemy, bossSpawn);
        g.GetComponent<Enemy>().destroyed.Subscribe(OnWinCon);
    }
}
