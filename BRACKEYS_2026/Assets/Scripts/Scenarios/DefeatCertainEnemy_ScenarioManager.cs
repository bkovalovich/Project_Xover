using UnityEngine;

public class DefeatCertainEnemy_ScenarioManager : Enemies_ScenarioManager {
    [SerializeField] GameObject bossEnemy;

    public override void SetupGame() {
        base.SetupGame();
        GameObject g = Instantiate(bossEnemy, GetRandomSpawnPoint(), Quaternion.identity);
        g.GetComponent<Enemy>().destroyed.Subscribe(OnWinCon);
    }
}
