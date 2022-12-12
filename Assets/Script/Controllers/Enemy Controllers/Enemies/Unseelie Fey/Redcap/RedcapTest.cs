using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapTest : Entity
{
    //Available States for this entity
    public RedcapIdleState idleState { get; private set; }
    public RedcapPatrolState patrolState { get; private set; }
    public RedcapChaseState chaseState { get; private set; }
    public RedcapRangedAttackState rangedAttackState { get; private set; }
    //public RedcapMeleeAttackState meleeAttackState { get; private set; }
    
    //Data
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_PatrolState patrolStateData;
    [SerializeField]
    private Data_ChaseState chaseStateData;
    [SerializeField]
    private Data_RangedAttackState rangedStateData;
    //[SerializeField]
    //private Data_MeleeAttackState meleeStateData;

    //Projectiles
    [SerializeField]
    private Rigidbody2D throwingRock;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        idleState = new RedcapIdleState(this, fsm, idleStateData, this);
        patrolState = new RedcapPatrolState(this, fsm, patrolStateData, this);
        chaseState = new RedcapChaseState(this, fsm, chaseStateData, this);
        rangedAttackState = new RedcapRangedAttackState(this, fsm, rangedStateData, this);
        //meleeAttackState = new RedcapMeleeAttackState(this, fsm, meleeAttackStateData, this);

        fsm.Initialize(patrolState);
    }

    public void redProjectile()
    {
        Rigidbody2D newThrowingRock = Instantiate(throwingRock, originAttackA.position, Quaternion.identity);
        newThrowingRock.velocity = new Vector3(facingDirection * rangedStateData.redProjectialSpeed, 0, 0); 
    }
}
