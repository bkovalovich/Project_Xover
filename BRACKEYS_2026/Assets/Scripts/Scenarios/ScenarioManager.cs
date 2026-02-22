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
    [SerializeField] public bool currentGame = false;
    [HideInInspector] public List<GameObject> otherScenarios;
    //TIMER
    [SerializeField] bool usesTimer;
    private Timer timer;
    [SerializeField] float maxTime;

    private void Awake()
    {
        // GetOtherScenarios(); 
    }

    public virtual void SetupGame()
    {
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
    public virtual void OnTimerFinish()
    {
        OnLoseCon();
    }

    public virtual void PlayerEnterGame()
    {
        currentGame = true;
        BroadcastScenarioState(currentGame);
        //Debug.Log("Player Entered Game");
    }
    public virtual void PlayerLeaveGame()
    {
        currentGame = false;
        BroadcastScenarioState(currentGame);
    }
    protected virtual void OnWinCon()
    {
        objectiveText.text = "Objective Complete!";
        objectiveText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 1);
        StartCoroutine(GameManager.instance.LoadNextScenario(true, this));
    }
    protected virtual void OnLoseCon()
    {
        objectiveText.text = "Objective Failed!";
        StartCoroutine(GameManager.instance.LoadNextScenario(false, this));
    }
    protected Vector2 GetRandomSpawnPoint()
    {
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
    public void GetOtherScenarios()
    {
        List<GameObject> scenarios = new List<GameObject>(GameObject.FindGameObjectsWithTag("ScenarioAnchor"));
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject scenario in scenarios)
        {
            var scenarioManager = scenario.GetComponentInChildren<ScenarioManager>();
            if (scenarioManager != null && scenarioManager.gameObject == this.gameObject)
            {
                toRemove.Add(scenario);
            }
        }
        foreach (GameObject scenario in toRemove)
        {
            scenarios.Remove(scenario);
        }
        otherScenarios = scenarios;
    }
    public void FinishScenario()
    {
        Destroy(this.gameObject);
    }

    protected void BroadcastScenarioState(bool isActive)
    {
        foreach (Transform child in transform)
        {
            var listeners = child.GetComponentsInChildren<IScenarioListener>();
            if (listeners != null)
            {
                foreach (var listener in listeners)
                {
                    listener.OnScenarioStateChanged(isActive);
                }
            }

        }
    }
}