using UnityEngine;

public abstract class ScenarioObject : MonoBehaviour {
    protected bool isWinCon;

    public virtual void Setup(bool isWinCon) {
        this.isWinCon = isWinCon;
    }
    protected void OnWinConComplete() {
        GameManager.instance.LoadNextGame(); 
    }
}
