using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapIdleState : IdleState
{
    private RedcapTest enemy;

    public RedcapIdleState(Entity ent, StateMachine fsm, Data_IdleState idleStateData, RedcapTest testEnemy) : base (ent, fsm, idleStateData)
    {
        enemy = testEnemy;
    }
    public override void Enter()
    {
        Debug.Log("Idle State Entered");
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleOver)
        {
            stateMachine.ChangeState(enemy.idleState);
        }    
    }
    public override void Exit()
    {
        Debug.Log("Idle State Exited");
        base.Exit();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
