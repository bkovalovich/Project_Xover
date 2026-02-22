using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public IEnumerator Warping()
    {
        player.rb.linearVelocity = Vector2.zero;
        player.warpTrajectory.ToggleWarp(true);

        GameObject targetScenario = null;
        GameObject finalTarget = null;

        while (player.holdingWarp)
        {
            var otherScenarios =
                GameManager.instance.CurrentScenario.otherScenarios;

            string targetId = null;

            Vector2 mouse = Mouse.current.position.ReadValue();

            float halfW = Screen.width * 0.5f;
            float halfH = Screen.height * 0.5f;

            if (mouse.x < halfW && mouse.y > halfH) targetId = "TL";
            else if (mouse.x > halfW && mouse.y > halfH) targetId = "TR";
            else if (mouse.x < halfW && mouse.y < halfH) targetId = "BL";
            else if (mouse.x > halfW && mouse.y < halfH) targetId = "BR";

            targetScenario = null;

            if (targetId != null)
            {
                foreach (var scenario in otherScenarios)
                {
                    ScenarioContainer c =
                        scenario.GetComponent<ScenarioContainer>();
                    //Debug.Log("Checking scenario " + scenario.name + " with ID " + c.ID);

                    if (c && c.ID == targetId)
                    {
                        targetScenario = scenario;
                        break;
                    }
                }
            }

            if (targetScenario != null)
            {
                finalTarget = targetScenario;
                //Debug.Log("Warping to " + finalTarget.name);
                player.warpTrajectory.transform.position =
                    targetScenario.transform.position;
            }

            yield return null;
        }

        player.warpTrajectory.ToggleWarp(false);

        if (finalTarget != null)
        {
            ScenarioManager targetScript =
                finalTarget.GetComponentInChildren<ScenarioManager>();

            if (targetScript != GameManager.instance.CurrentScenario)
            {
                GameManager.instance.SpawnPlayerInScenario(targetScript.gameObject);
                StartCoroutine(player.Invulnerability(0.7f));
                yield return new WaitForFixedUpdate();
                player.warpTrajectory.Burst(player.rb.position);
            }
        }

        player.stateMachine.ChangeToDefault();
    }

}
