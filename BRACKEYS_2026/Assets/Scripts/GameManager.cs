using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening; 

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
        GameObject startingScenario = null;
        for (int i = 0; i < spawnPoints.Length; i++) {
            GameObject scenario = Instantiate(PickNextScenario(), spawnPoints[i].transform);
            scenario.GetComponent<ScenarioManager>().SetupGame();
            if (i == 0) startingScenario = scenario;                  
        }
        SpawnPlayerInScenario(startingScenario);
        
    }
    public void PlayerTakeDamage() {
        health--;
        mainCanvas.Health = health; 
    }
    public void SpawnPlayerInScenario(GameObject scenario) {
        player.transform.position = scenario.transform.position;
        ScenarioManager scenarioScript = scenario.GetComponent<ScenarioManager>();
        if (scenarioScript == null)
        {
            Debug.LogError("ScenarioManager component not found on scenario GameObject.");
            return;
        }
        TellScenariosWhichIsActive(scenarioScript);
    }
    private GameObject PickNextScenario() {
        return scenarioList[Random.Range(0, scenarioList.Length)];
    }
    public IEnumerator LoadNextScenario(bool successful, ScenarioManager sceneToLoad) {
        yield return new WaitForSeconds(1f);

        ScenarioManager completed = sceneToLoad; //finish previous scenario
        ScenarioContainer container = completed.transform.parent.GetComponent<ScenarioContainer>();

        Image overlay = container.overlay; //start overlay transition
        Tween tween = overlay.DOFade(1, 1);
        container.particles.Play();
        yield return tween.WaitForCompletion();
        Debug.Log("got past wait for complete");

        completed.FinishScenario(); 
        GameObject newScenario = Instantiate(PickNextScenario(), container.transform); //spawn new scenario
        newScenario.GetComponent<ScenarioManager>().SetupGame();
        if (currentScenario == sceneToLoad)
            SpawnPlayerInScenario(newScenario);
        else
            currentScenario.GetOtherScenarios();
        //yield return new WaitForSeconds(1);
        tween = overlay.DOFade(0, 1);
        container.particles.Pause(); 
        yield return tween.WaitForCompletion();


    }

    public void TellScenariosWhichIsActive(ScenarioManager scenarioScript)
    {
        if (scenarioScript == null)
        {
            Debug.LogError("scenarioScript is null.");
            return;
        }
        scenarioScript.PlayerEnterGame();
        scenarioScript.GetOtherScenarios();
        int i;
        for (i = 0; i < scenarioScript.otherScenarios.Count; i++)
        {
            var otherScenarioGO = scenarioScript.otherScenarios[i];
            if (otherScenarioGO == null)
            {
                Debug.LogError($"otherScenarios[{i}] is null");
                continue;
            }
            var otherScenarioManager = otherScenarioGO.GetComponentInChildren<ScenarioManager>();
            if (otherScenarioManager == null)
            {
                Debug.LogError($"ScenarioManager not found in children of {otherScenarioGO.name}");
                continue;
            }
            otherScenarioManager.PlayerLeaveGame();
        }
        CurrentScenario = scenarioScript;
    }

}
