using UnityEngine;

public class DefeatCertainEnemy_ScenarioManager : ScenarioManager {
    [SerializeField] GameObject bossEnemy, smallerEnemy;

    public override void SetupGame() {
        GameObject g = Instantiate(bossEnemy, GetRandomSpawnPoint(), Quaternion.identity);

        g.GetComponent<Enemy>().destroyed.Subscribe(OnWinCon);

    }

    protected override void OnWinCon() {
        throw new System.NotImplementedException();
    }
}
