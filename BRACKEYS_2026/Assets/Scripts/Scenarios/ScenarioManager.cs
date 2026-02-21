using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; 

public abstract class ScenarioManager : MonoBehaviour
{
   [HideInInspector] public Camera scenarioCamera;
   [HideInInspector] public Canvas scenarioCanvas;

    [SerializeField] SpriteRenderer backgroundSprite;
    [SerializeField] string objective;
    [SerializeField] Color textColor; 

    protected TMP_Text objectiveText; 
    //protected int ID;
    
    public Event WinConCompleted = new Event();
    protected bool currentGame;
    [HideInInspector] public List<GameObject> otherScenarios;
    //TIMER
    [SerializeField] bool usesTimer;
    private Timer timer;
    [SerializeField] float maxTime; 

    private void Awake() {
        GetOtherScenarios(); 
    }

    public virtual void SetupGame() {
        scenarioCamera = transform.parent.GetComponentInChildren<Camera>();
        scenarioCanvas = transform.parent.GetComponentInChildren<Canvas>();
        objectiveText = gameObject.transform.parent.GetComponentInChildren<TMP_Text>();
        objectiveText.color = textColor;
        timer = scenarioCanvas.GetComponentInChildren<Timer>();
        if (usesTimer)
            timer?.StartTimer(maxTime, this);
        else
            timer?.Toggle(false); 
    }
    public virtual void OnTimerFinish() {
        OnLoseCon(); 
    }

    public virtual void PlayerEnterGame() {
        currentGame = true;
    }
    public virtual void PlayerLeaveGame() {
        currentGame = false; 
    }
    protected virtual void OnWinCon() {
        objectiveText.text = "Objective Complete!";
        objectiveText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 1); 
        StartCoroutine(GameManager.instance.LoadNextScenario(true));
    }
    protected virtual void OnLoseCon() {
        objectiveText.text = "Objective Failed!";
        StartCoroutine(GameManager.instance.LoadNextScenario(false));
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
