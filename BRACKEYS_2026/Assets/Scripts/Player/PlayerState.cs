using UnityEngine;

public abstract class PlayerState : MonoBehaviour {
    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    private void Awake() {
        player = GetComponent<Player>();
        playerStateMachine = player.stateMachine;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void FrameUpdate();
    public abstract void PhysicsUpdate();
}
