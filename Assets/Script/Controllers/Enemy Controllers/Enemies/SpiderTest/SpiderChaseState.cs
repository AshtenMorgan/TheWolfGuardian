/*
 * The Chase State for the test Spider.
 * handles what happens while the spider
 * is in chase state.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderChaseState : ChaseState
{
    private SpiderTest enemy;

    public SpiderChaseState(Entity ent, StateMachine fsm, Data_ChaseState chaseStateData, SpiderTest testEnemy) : base(ent, fsm, chaseStateData)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        entity.PlayerDetected = true;
    }

    public override void Exit()
    {
        base.Exit();
        entity.PlayerDetected = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!enemy.CanSeeTarget())
            stateMachine.ChangeState(enemy.idleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.SetVelocity(stateData.runSpeed);
    }
    
}
