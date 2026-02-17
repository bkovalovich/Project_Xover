using UnityEngine;

public abstract class ScenarioObject : MonoBehaviour {
    protected bool isWinCon;
    protected Event winConCompleted; 

    public virtual void Setup(bool isWinCon, Event winConCompleted) {
        this.isWinCon = isWinCon;
        this.winConCompleted = winConCompleted; 
    }
    protected void OnWinConComplete() {
        GameManager.instance.LoadNextGame(); 
    }
}
