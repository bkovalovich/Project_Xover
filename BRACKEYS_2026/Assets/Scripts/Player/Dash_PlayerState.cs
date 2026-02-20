using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Dash_PlayerState : PlayerState
{
    private float dashSpeed, dashLength; 

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
        float time = 0, duration = player.playerInfo.dashLength;
        Vector2 input = player.currentMouseInput == Vector2.zero ? Vector2.up : player.currentMoveInput;
        input = input.normalized;

        dashSpeed = player.playerInfo.dashSpeed;
        player.rb.linearVelocity = input * new Vector2(dashSpeed, dashSpeed) * Time.deltaTime;
        while (time < duration) {
            time += Time.deltaTime;
            continue; 
        }
        player.stateMachine.ChangeToDefault(); 
    }

}
