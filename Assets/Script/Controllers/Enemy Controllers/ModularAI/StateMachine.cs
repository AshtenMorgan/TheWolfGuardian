/* 
 * 
 * The manager for the State Machine
 * holds functions for entering,
 * exiting, or changing states
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState { get; private set; }

    //the starting state
    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();//exit state
        currentState = newState;//change state
        currentState.Enter();//begin state
    }
}
