using UnityEngine;

public abstract class ScenarioManager : MonoBehaviour
{
    //protected int ID;
    
    public Event WinConCompleted = new Event();
    protected GameManager gm; 
    protected bool currentGame;

    protected virtual void SetupGame(GameManager gm) {
        this.gm = gm; 
    }
    protected virtual void EnterGame() {
        currentGame = true;
    }
    protected virtual void LeaveGame() {
        currentGame = false; 
    }
    protected abstract void OnWinCon();

}
