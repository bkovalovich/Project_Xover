using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private int score;
    public GameObject player; 

    [SerializeField] GameObject[] scenarioList;
    [SerializeField] GameObject[] spawnPoints; 

    private void Awake() {
       instance = this;
       player = GameObject.Find("Player");

        for (int i = 0; i < spawnPoints.Length; i++) {
            GameObject g = Instantiate(PickNextScenario(), spawnPoints[i].transform.position, Quaternion.identity);
            g.GetComponent<ScenarioManager>().SetupGame();
        }
    }
    private GameObject PickNextScenario() {
        return scenarioList[Random.Range(0, scenarioList.Length - 1)];
    }
    public void LoadNextGame() {
        GameObject scenario = PickNextScenario(); 
    }
}
