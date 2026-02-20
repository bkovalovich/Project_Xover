using UnityEngine;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text score, health; 
    
    public int Score {
        set { score.text = $"Score: {value}"; }
    }
    public int Health {
        set { health.text = $"Health: {value}"; }
    }
}
