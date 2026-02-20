using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Move_PlayerState : PlayerState
{
    private Vector2 lastMoveSpeed;
    private float currentSpeed;
    private bool decreaseIsRunning = false;


    private void OnEnable() {
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
        Vector2 input = player.currentMoveInput.normalized;
        float accelRate = (input != Vector2.zero) ? player.playerInfo.acceleration : player.playerInfo.deacceleration;

        Vector2 targetVel = player.currentMoveInput.normalized * player.playerInfo.moveSpeed;
        player.rb.linearVelocity = Vector2.MoveTowards(player.rb.linearVelocity,targetVel,accelRate * Time.deltaTime);
    }

}
