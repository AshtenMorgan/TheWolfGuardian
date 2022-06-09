/* 
 * Ranged Attack state, rolls a random number, used to determine
 * attack A or B, then creates prefab and "throws" it in direction
 * entity is facing 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangedAttack : RangedAttackState
{
    private SpiderTest enemy;
    public SpiderRangedAttack(Entity ent, StateMachine fsm, Data_RangedAttackState data, SpiderTest testEnemy) : base(ent, fsm, data)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        //animation
        base.Enter();
    }

    public override void Exit()
    {
        //fire web
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
