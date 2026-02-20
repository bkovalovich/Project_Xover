using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine {
    private PlayerState currentState;
    public PlayerState CurrentState { get; set;}
    public PlayerState defaultState;

    public void Initialize(PlayerState startingState) {
        CurrentState = startingState;
        defaultState = startingState; 
        CurrentState.EnterState();
    }
    public void ChangeState(PlayerState newState) {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    public void ChangeToDefault() {
        CurrentState.ExitState();
        CurrentState = defaultState;
        CurrentState.EnterState(); 
    }
    public bool IsInState(PlayerState state) {
        return currentState == state; 
    }
}
