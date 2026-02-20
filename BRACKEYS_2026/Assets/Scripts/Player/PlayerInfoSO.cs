using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo")]

public class PlayerInfoSO : ScriptableObject {
    [SerializeField] public float moveSpeed, acceleration, 
        deacceleration, dashSpeed, attackLength, dashLength, knockbackLength, knockbackStrength;
    [SerializeField] public int health;
}
