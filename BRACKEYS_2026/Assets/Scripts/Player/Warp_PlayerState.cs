using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_PlayerState : PlayerState {

    private List<GameObject> otherScenarios;

    public override void EnterState() {
        StartCoroutine(Warping());
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
    }

    public IEnumerator Warping() {
        //Debug.Log("Warping called");
        otherScenarios = GameManager.instance.CurrentScenario.otherScenarios;

        player.rb.linearVelocity = Vector2.zero;

        GameObject closestScenario = null;
        player.warpTrajectory.ToggleWarp(true);
        while (player.holdingWarp) {
            closestScenario = VectorUtilities.GetClosestGameObject(player.currentMouseWorldInput, otherScenarios);
            player.warpTrajectory.gameObject.transform.position = closestScenario.transform.position;
            yield return null;
        }
        player.warpTrajectory.ToggleWarp(false);

        ScenarioManager closestScript = closestScenario?.GetComponentInChildren<ScenarioManager>();
        if (closestScript != GameManager.instance.CurrentScenario) {
            GameManager.instance.CurrentScenario = closestScript;
            player.rb.position = closestScript.gameObject.transform.position;
            StartCoroutine(player.Invulnerability(0.7f));
            player.warpTrajectory.Burst(player.rb.position);

        }
        player.stateMachine.ChangeToDefault();
    }

}
