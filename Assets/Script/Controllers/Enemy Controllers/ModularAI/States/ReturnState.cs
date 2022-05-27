/*
 * Master Return State
 * Things that may be useful for all return states
 * nothing here yet
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : State
{
    protected Data_ReturnState stateData;
    public ReturnState(Entity ent, StateMachine fsm, Data_ReturnState returnStateData) : base(ent, fsm)
    {
        stateData = returnStateData;
    }
}
