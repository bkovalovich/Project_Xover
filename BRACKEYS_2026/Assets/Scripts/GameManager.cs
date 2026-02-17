using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    [SerializeField] GameObject[] scenarioList;

    private GameObject PickNextScenario() {
        return null;
    }
    private void LoadNextGame() {
        GameObject scenario = PickNextScenario(); 
    }
}
