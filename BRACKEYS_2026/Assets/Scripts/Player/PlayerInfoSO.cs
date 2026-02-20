using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo")]

public class PlayerInfoSO : ScriptableObject {
    [SerializeField] public float health, moveSpeed, acceleration, 
        deacceleration, dashSpeed, attackLength, dashLength, knockbackLength, knockbackStrength; 
    
}
