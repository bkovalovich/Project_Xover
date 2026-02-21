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
    private int score, health;

    [SerializeField] GameObject[] scenarioList;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] MainCanvas mainCanvas; 

    private void Awake() {
       instance = this;
        score = 0;
        mainCanvas.Score = score;
        health = player.GetComponent<Player>().playerInfo.health;
        mainCanvas.Health = health; 
        for (int i = 0; i < spawnPoints.Length; i++) {
            GameObject scenario = Instantiate(PickNextScenario(), spawnPoints[i].transform);
            scenario.GetComponent<ScenarioManager>().SetupGame();
            if (i == 0) SpawnPlayerInScenario(scenario);                   
        }
        
    }
    public void PlayerTakeDamage() {
        health--;
        mainCanvas.Health = health; 
    }
    private void SpawnPlayerInScenario(GameObject scenario) {
        player.transform.position = scenario.transform.position;
        ScenarioManager scenarioScript = scenario.GetComponent<ScenarioManager>();
        scenarioScript.PlayerEnterGame();
        CurrentScenario = scenarioScript;         
    }
    private GameObject PickNextScenario() {
        return scenarioList[Random.Range(0, scenarioList.Length)];
    }
    //IEnumerator LoadNextScenario(bool successful) {

    //}
    public void LoadNextScenario(bool successful) {
        Debug.Log("next scenario loaded");
        ScenarioManager completed = currentScenario; //finish previous scenario
        Transform container = completed.transform.parent; 
        completed.FinishScenario(); 

        GameObject newScenario = Instantiate(PickNextScenario(), container); //spawn new scenario
        newScenario.GetComponent<ScenarioManager>().SetupGame(); 
        SpawnPlayerInScenario(newScenario);
    }


}
