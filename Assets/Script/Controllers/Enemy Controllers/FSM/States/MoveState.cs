using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected Data_MoveState stateData;

    protected bool 
        isDetectingWall,
        isDetectingLedge;
    public MoveState(Entity ent, FiniteStateMachine fsm, Data_MoveState moveStateData) : base(ent, fsm)
    {
        stateData = moveStateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.moveSpeed);//set velocity to movement speed

        isDetectingLedge = entity.CheckLedge();//look for ledge
        isDetectingWall = entity.CheckWall();//look for wall
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
