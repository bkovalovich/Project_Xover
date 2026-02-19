using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioManager : MonoBehaviour
{
    public Camera scenarioCamera; 
    [SerializeField] SpriteRenderer backgroundSprite; 
    //protected int ID;
    
    public Event WinConCompleted = new Event();
    protected bool currentGame;
    public List<GameObject> otherScenarios; 

    private void Awake() {
        GetOtherScenarios(); 
    }

    public virtual void SetupGame() {
        scenarioCamera = transform.parent.GetComponentInChildren<Camera>(); 
    }

    public virtual void PlayerEnterGame() {
        currentGame = true;
    }
    public virtual void PlayerLeaveGame() {
        currentGame = false; 
    }
    protected abstract void OnWinCon();

    protected Vector2 GetRandomSpawnPoint() {
        Bounds bounds = backgroundSprite.bounds;
        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minY = bounds.min.y;
        float maxY = bounds.max.y;

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    protected void GetOtherScenarios() {
        List<GameObject> scenarios = new List<GameObject>(GameObject.FindGameObjectsWithTag("ScenarioAnchor"));
        foreach (GameObject scenario in scenarios) {
            if (scenario == this.gameObject) {
                scenarios.Remove(scenario);
            }
        }
        Debug.Log(scenarios);
        otherScenarios = scenarios; 
    }
}
