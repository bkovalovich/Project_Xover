using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfoSO playerInfo;
    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerInfo = playerInfo;
    }

}
