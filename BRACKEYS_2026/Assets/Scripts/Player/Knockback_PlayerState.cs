using UnityEngine;
using System.Collections;

public class Knockback_PlayerState : PlayerState {
    private float strength, length;
    private Color defaultColor; 
    [SerializeField] Color knockbackColor; 
    public override void EnterState() {
        strength = player.playerInfo.knockbackStrength;
        length = player.playerInfo.knockbackLength;
        defaultColor = player.sr.color; 
        player.sr.color = knockbackColor; 
        StartCoroutine(Knockback());
    }

    public override void ExitState() {
        player.sr.color = defaultColor; 
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {    
    }
    IEnumerator Knockback() {
        Debug.Log("start"); 
        float current = 0;
        Vector2 knockbackDirection = ((Vector2)transform.position - player.pointOfLastCollision).normalized;

        player.rb.AddForce(player.currentMoveInput.normalized * strength, ForceMode2D.Force);
        while(current < length) {
            current += Time.deltaTime; 
            yield return new WaitForSeconds(Time.deltaTime);
        }
        playerStateMachine.ChangeToDefault(); 
    }
}
