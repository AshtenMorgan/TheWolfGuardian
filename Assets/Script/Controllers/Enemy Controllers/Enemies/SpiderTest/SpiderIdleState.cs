/*
 * The Idle state for the test Spider.
 * This state currently just waits for isIdleOver
 * to be true, and then switches back to patrol
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdleState : IdleState
{
    private SpiderTest enemy;
    public SpiderIdleState(Entity ent, StateMachine fsm, Data_IdleState data, SpiderTest testEnemy) : base(ent, fsm, data)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleOver)
            stateMachine.ChangeState(enemy.patrolState);
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
