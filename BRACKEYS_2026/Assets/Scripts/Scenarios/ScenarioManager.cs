using UnityEngine;

public abstract class ScenarioManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundSprite; 
    //protected int ID;
    
    public Event WinConCompleted = new Event();
    protected bool currentGame;

    public abstract void SetupGame();

    protected virtual void PlayerEnterGame() {
        currentGame = true;
    }
    protected virtual void PlayerLeaveGame() {
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
}
