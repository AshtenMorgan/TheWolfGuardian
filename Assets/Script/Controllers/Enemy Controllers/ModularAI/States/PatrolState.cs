/* 
 * Basics for a Patrol state - Includes the checks for walls and ledges
 * movement should be handled in the specific enemy patrol sate 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    protected Data_PatrolState stateData;

    public PatrolState(Entity ent, FiniteStateMachine fsm, Data_PatrolState patrolStateData) : base(ent, fsm)
    {
        stateData = patrolStateData;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void Exit()
    {
        base.Exit();
    }


}
