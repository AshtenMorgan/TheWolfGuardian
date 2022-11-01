/*
 * This is the AI "Controller"
 * If a state should be available to a specific enemy, it
 * should be listed here
 * 
 * Under developement
 */
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class SpiderMeleeAttackState: MeleeAttackState
{
  
    private float attackTime;

    private SpiderTest enemy;
    public SpiderMeleeAttackState(Entity ent, StateMachine fsm, Data_MeleeAttackState data, SpiderTest testEnemy) : base(ent, fsm, data)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        enemy.ani.SetBool("insideAttackDistance", true);
        enemy.ani.SetTrigger("fireRangedAttack");
        attackTime = Time.time;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (!enemy.CanSeeTarget())
            stateMachine.ChangeState(enemy.idleState);

        if (Time.time >= (attackTime + stateData.attackCooldown))
            DetermineAttack();

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void DetermineAttack()
    {
        int check = Random.Range(0, 101);
        if (check > 49)
        {
            enemy.RangedAttackA();
            attackTime = Time.time;
        }
        else
        {
            enemy.RangedAttackB();
            attackTime = Time.time;
        }
    }
}
