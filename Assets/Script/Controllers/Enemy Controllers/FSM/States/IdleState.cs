using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected Data_IdleState stateData;
    protected bool shouldFlip,
        isIdleOver;
    protected float idleTime;


    public IdleState(Entity ent, FiniteStateMachine fsm, Data_IdleState data) : base(ent, fsm)
    {
        stateData = data;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);//stop moving
        isIdleOver = false;
        SetRandomIdleTime();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //check timer
        if (Time.time >= startTime + idleTime)
            isIdleOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        if (shouldFlip)
            entity.Flip();
    }
    public void SetFlip(bool flip)
    {
        shouldFlip = flip;
    }
    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
