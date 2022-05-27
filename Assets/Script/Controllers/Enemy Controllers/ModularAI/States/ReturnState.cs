using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : State
{
    protected Data_ReturnState stateData;
    public ReturnState(Entity ent, FiniteStateMachine fsm, Data_ReturnState returnStateData) : base(ent, fsm)
    {
        stateData = returnStateData;
    }
}
