using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private int score;

    [SerializeField] GameObject[] scenarioList;

    private void Awake() {
        instance = this;
    }
    private GameObject PickNextScenario() {
        return null;
    }
    public void LoadNextGame() {
        GameObject scenario = PickNextScenario(); 
    }
}
