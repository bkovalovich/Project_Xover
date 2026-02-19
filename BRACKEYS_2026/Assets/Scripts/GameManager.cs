using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager instance { get; private set; }
    private ScenarioManager currentScenario;
    [HideInInspector] public Camera currentCamera; 
    [SerializeField] public GameObject player; 

    public ScenarioManager CurrentScenario {
        get { return currentScenario; }
        set {
            currentScenario = value;
            currentCamera = value.scenarioCamera; 
        }
    }
    private int score;

    [SerializeField] GameObject[] scenarioList;
    [SerializeField] GameObject[] spawnPoints; 

    private void Awake() {
       instance = this;
        for (int i = 0; i < spawnPoints.Length; i++) {
            GameObject scenario = Instantiate(PickNextScenario(), spawnPoints[i].transform);
            scenario.GetComponent<ScenarioManager>().SetupGame();
            if (i == 0) SpawnPlayerInScenario(scenario);                   
        }
        
    }
    private void SpawnPlayerInScenario(GameObject scenario) {
        player.transform.position = scenario.transform.position;
        ScenarioManager scenarioScript = scenario.GetComponent<ScenarioManager>();
        scenarioScript.PlayerEnterGame();
        CurrentScenario = scenarioScript;         
    }
    private GameObject PickNextScenario() {
        return scenarioList[Random.Range(0, scenarioList.Length - 1)];
    }
    public void LoadNextGame() {
        GameObject scenario = PickNextScenario(); 
    }

}
