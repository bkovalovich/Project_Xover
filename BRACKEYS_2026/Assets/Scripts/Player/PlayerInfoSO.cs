using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo")]

public class PlayerInfoSO : ScriptableObject {
    [SerializeField] public float health, moveSpeed, dashSpeed, attackLength; 
    
}
