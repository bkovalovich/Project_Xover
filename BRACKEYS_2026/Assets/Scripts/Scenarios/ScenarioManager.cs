using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; 

public abstract class ScenarioManager : MonoBehaviour
{
    public Camera scenarioCamera;
    public Canvas scenarioCanvas;
    [SerializeField] SpriteRenderer backgroundSprite;
    [SerializeField] string objective;
    [SerializeField] Color textColor; 

    protected TMP_Text objectiveText; 
    //protected int ID;
    
    public Event WinConCompleted = new Event();
    protected bool currentGame;
    [HideInInspector] public List<GameObject> otherScenarios; 

    private void Awake() {
        GetOtherScenarios(); 
    }

    public virtual void SetupGame() {
        Debug.Log("fork off");
        scenarioCamera = transform.parent.GetComponentInChildren<Camera>();
        scenarioCanvas = transform.parent.GetComponentInChildren<Canvas>();
        objectiveText = gameObject.transform.parent.GetComponentInChildren<TMP_Text>();
        //objectiveText.text = objective;
        objectiveText.color = textColor; 
    }

    public virtual void PlayerEnterGame() {
        currentGame = true;
    }
    public virtual void PlayerLeaveGame() {
        currentGame = false; 
    }
    protected virtual void OnWinCon() {
        objectiveText.text = "Objective Complete!";

        GameManager.instance.LoadNextScenario(true);
    }
    protected virtual void OnLoseCon() {
        objectiveText.text = "Objective Failed!";
        GameManager.instance.LoadNextScenario(false);
    }
    protected Vector2 GetRandomSpawnPoint() {
        Bounds bounds = backgroundSprite.bounds;
        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    protected Vector2 GetScreenCenter()
    {
        return backgroundSprite.bounds.center;
    }
    protected void GetOtherScenarios() {
        List<GameObject> scenarios = new List<GameObject>(GameObject.FindGameObjectsWithTag("ScenarioAnchor"));
        foreach (GameObject scenario in scenarios) {
            if (scenario == this.gameObject) {
                scenarios.Remove(scenario);
            }
        }
        otherScenarios = scenarios; 
    }
    public void FinishScenario() {
        Destroy(this.gameObject);
    }

}
