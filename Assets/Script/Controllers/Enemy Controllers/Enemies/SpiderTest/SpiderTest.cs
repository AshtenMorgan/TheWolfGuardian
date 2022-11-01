/*
 * This is the AI "Controller"
 * If a state should be available to a specific enemy, it
 * should be listed here
 * 
 * Under developement
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTest : Entity
{
    //list all available states for this entity
    public SpiderIdleState idleState { get; private set; }
    public SpiderPatrolState patrolState { get; private set; }
    public SpiderChaseState chaseState { get; private set; }
    public SpiderRangedAttackState rangedAttackState { get; private set; }
    public SpiderMeleeAttackState meleeAttackState { get; private set; }

    //state data
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_PatrolState patrolStateData;
    [SerializeField]
    private Data_ChaseState chaseStateData;
    [SerializeField]
    private Data_RangedAttackState rangedAttackStateData;
    [SerializeField]
    private Data_MeleeAttackState meleeAttackStateData;

    //Projectiles
    [SerializeField]
    private Rigidbody2D webShot;
    [SerializeField]
    private Rigidbody2D venomShot;

    public override void Start()
    {
        base.Start();
        idleState = new SpiderIdleState(this, fsm, idleStateData, this);
        patrolState = new SpiderPatrolState(this, fsm, patrolStateData, this);
        chaseState = new SpiderChaseState(this, fsm, chaseStateData, this);
        rangedAttackState = new SpiderRangedAttackState(this, fsm, rangedAttackStateData, this);
        meleeAttackState = new SpiderMeleeAttackState(this, fsm, meleeAttackStateData, this);

        fsm.Initialize(patrolState);
    }

    public void RangedAttackA()
    {
        Rigidbody2D newwebShot = Instantiate(webShot, originAttackA.position, Quaternion.identity);
        newwebShot.velocity = new Vector3(facingDirection *rangedAttackStateData.projectialSpeedA, 0, 0);
        //play sound
    }
    public void RangedAttackB()
    {
        
        Rigidbody2D newvenomShot = Instantiate(venomShot, originAttackB.position, Quaternion.identity);
        newvenomShot.velocity = new Vector3(facingDirection * rangedAttackStateData.projectialSpeedB,0,0);
        //play sound
    }
    public void MeleeAttack()
    {
        //code to switch animations to melee atack, also to add lunge physics
    }
}
