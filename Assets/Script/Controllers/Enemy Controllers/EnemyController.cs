using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : Controller
{
    #region vars
    //vars to be visable for testing purposes only.
    #region StateMachine

    protected enum State
    {
        Patrol,
        Chase,
        Flee,
        MeleeAttack,
        RangedAttack,
        Hurt,
        Dead
    }
    [SerializeField]
    protected State currentState = State.Patrol;
    #endregion

    #region objects
    [Header("Enemy Variables")]
    [SerializeField]
    protected PlayerPawn target;
    [SerializeField]
    protected EnemyPawn pawn;
    [SerializeField]
    protected Health health;
    [SerializeField]
    protected ECombat eCombat;
    [SerializeField]
    protected Transform eyeball,
        eGroundCheck,
        playerCheck;
    [SerializeField, Header("Movement stats")]
    protected Vector2 movement,
                      knockBack;

    //LayerMask groundLayer
    [SerializeField]
    private LayerMask enemyLayer,
        playerLayer;
    #endregion
    #region General variables
    [SerializeField]
    protected int facingDirection,
        damageDirection;

    [SerializeField]
    protected float patrolRange = 5.0f,
        groundCheckDistance = 1.0f,
        wallCheckDistance = 1.0f,
        enemyCheckDistance = 1.0f,
        harmStart,
        harmDuration,
        meleeDistance = 1.0f,
        rangedDistance = 3.0f,
        fleeDistance = 5.0f;

    [SerializeField]
    public bool isGroundDetected = true,
        isWallDetected = false,
        shouldMove = false,
        isPlayerDetected = false,
        hasRangedAttack = false,
        isInMeleeRange = false,
        isInRangedRange = false;

    [SerializeField]
    protected Vector3 playerCheckDistance;


    #endregion

    #endregion

    #region functions

    #region start/update
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();//call parents start function
        pawn = GetComponent<EnemyPawn>();//refrence this objects enemy pawn
        health = GetComponent<Health>();//get health object
        ani = GetComponent<Animator>();//get animator component
        eCombat = GetComponent<ECombat>();
        target = GameManager.Instance.Player;//get player from game manager
        facingDirection = 1;//face right
;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//calls whatever is in parents update function
    }

    protected override void FixedUpdate()
    {
        #region State Machine
        switch (currentState)
        {
            case State.Patrol:
                UpdatePatrolState();
                break;
            case State.Chase:
                UpdateChaseState();
                break;
            case State.Flee:
                UpdateFleeState();
                break;
            case State.MeleeAttack:
                UpdateMeleeAttackState();
                break;
            case State.RangedAttack:
                UpdateRangedAttackState();
                break;
            case State.Hurt:
                UpdateHurtState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        #endregion
        base.FixedUpdate();//call parent function   
    }
    #endregion



    #region States
    #region Patrol 
    // Patrol State
    protected virtual void EnterPatrolState()
    {

    }
    protected virtual void UpdatePatrolState()
    {
        StepDetection();//check if we can take a step  
        Walk();//move
    }
    protected virtual void ExitPatrolState()
    {

    }
    #endregion

    #region Chase
    //Chase State
    protected virtual void EnterChaseState()
    {

    }
    protected virtual void UpdateChaseState()
    {
        Chase();
    }
    protected virtual void ExitChaseState()
    {

    }
    #endregion

    #region Flee
    //Flee State
    protected virtual void EnterFleeState()
    {

    }
    protected virtual void UpdateFleeState()
    {
        Flee();
    }
    protected virtual void ExitFleeState()
    {

    }
    #endregion

    #region Attack
    protected virtual void EnterMeleeAttackState()
    {

    }
    protected virtual void UpdateMeleeAttackState()
    {
        Chase();//continue running towards player
        MeleeAttack();//attack player
    }
    protected virtual void ExitMeleeAttackState()
    {

    }

    protected virtual void EnterRangedAttackState()
    {

    }
    protected virtual void UpdateRangedAttackState()
    {
        RangedAttack();
    }
    protected virtual void ExitRangedAttackState()
    {

    }
    #endregion

    #region Hurt
    //Hurt State
    protected virtual void EnterHurtState()
    {
        harmStart = Time.time;//track when enemy was hit
        //hit animation
        //apply a knockback/stun/slow
    }
    protected virtual void UpdateHurtState()
    {
        if (Time.time >= harmStart + harmDuration)
        {
            StateManager(State.Patrol);
        }
        if (health.percent >= .15)
        {
            StateManager(State.Flee);
        }
    }
    protected virtual void ExitHurtState()
    {

    }
    #endregion

    #region Dead
    //Dead State
    protected virtual void EnterDeadState()
    {
        //stuff to do on death
        //this is already mostly handled by object pool and health kill function already
    }
    protected virtual void UpdateDeadState()
    {

    }
    protected virtual void ExitDeadState()
    {

    }
    #endregion
    #endregion
    #region State Manager
    protected virtual void StateManager(State state)
    {
        //exit current state
        switch (state)
        {
            case State.Patrol:
                ExitPatrolState();
                break;
            case State.Chase:
                ExitChaseState();
                break;
            case State.Flee:
                ExitFleeState();
                break;
            case State.MeleeAttack:
                ExitMeleeAttackState();
                break;
            case State.RangedAttack:
                ExitRangedAttackState();
                break;
            case State.Hurt:
                ExitHurtState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        //enter new state
        switch (state)
        {
            case State.Patrol:
                EnterPatrolState();
                break;
            case State.Chase:
                EnterChaseState();
                break;
            case State.Flee:
                EnterFleeState();
                break;
            case State.MeleeAttack:
                EnterMeleeAttackState();
                break;
            case State.RangedAttack:
                EnterRangedAttackState();
                break;
            case State.Hurt:
                EnterHurtState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;//update state
    }

    #endregion

    //this way we can see what direction an attack comes from (along with other details)
    // may not actually use this but it is worth thinking about
    protected virtual void Damage(float[] attackDetails)
    {
        /*
        enemy.currentHealth -= attackDetails[0];
        if (attackDetails[1] > enemy.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //other hit effects
        if (enemy.currentHealth > 0.0f )
        {
            StateManager(State.Hurt);
        }
        else if (enemy.currentHealth < 0.0f)
        {
            StateManager(State.Dead);
        }
        */
    }

    #region Motions
    protected virtual void Flip()
    {
        Vector2 flip = gameObject.transform.localScale;
        flip.x *= -1;
        gameObject.transform.localScale = flip;
        facingDirection *= -1;
        movement.Set(pawn.WalkSpeed * facingDirection, rb2d.velocity.y);//set speed to walk
        rb2d.velocity = movement;//set velocity to movement speed/direction
    }
    protected virtual void StepDetection()
    {
        isGroundDetected = Physics2D.Raycast(eGroundCheck.position, Vector2.down, groundCheckDistance, groundLayer);//raycast for ground
        isWallDetected = Physics2D.Raycast(eyeball.position, transform.right, wallCheckDistance, groundLayer);//raycast for wall
    }
    protected virtual void Walk()
    {
        if (isGrounded)//only do actions while on the ground
        {
            Locate();//look for obstacles
            if (!isGroundDetected || isWallDetected)//there is no ground or there is a wall
            {
                Flip();//turn around
            }
            else if (isPlayerDetected)//we see the player
            {
                StateManager(State.Chase);
            }
            else//we have a ground, no wall, and no enemy, no player
            {
                movement.Set(pawn.WalkSpeed * facingDirection, rb2d.velocity.y);//set speed to walk
                rb2d.velocity = movement;//set velocity to movement speed/direction
            }
        }
    }
    protected virtual void Chase()
    {
        Locate();//continue looking for player
        if (isPlayerDetected)
        {
            FacePlayer();//look at player
            RangeCheck();//check distance
            MoveRangeCheck();
            StepDetection();
            if (shouldMove)
            {
                movement.Set(pawn.RunSpeed * facingDirection, rb2d.velocity.y);//set movement speed to run
                rb2d.velocity = movement;//set velocity to movement speed/direction


                if (!isGroundDetected)
                {
                    Jump();//should attempt to jump gaps
                }
            }
        }
        else if (!isPlayerDetected)
        {
            StateManager(State.Patrol);//return to patrol if we cant see player anymore
        }
    }
    protected virtual void Locate()
    {
        Collider2D Collider = Physics2D.OverlapBox(playerCheck.position, playerCheckDistance, 0, playerLayer);
        if (Collider != null)//overlap hit something
        {
            isPlayerDetected = Physics2D.Raycast(eyeball.transform.position, target.transform.position, playerLayer);
        }
        else
        {
            isPlayerDetected = false;
        }
    }
    protected virtual void FacePlayer()
    {
        if ((target.transform.position.x < transform.position.x) && (facingDirection == 1))//player is on the left of enemy, and enemy is facing right
        {
            Flip();
        }
        else if ((target.transform.position.x < transform.position.x) && (facingDirection == -1))//player on left, enemy facing left
        {
            return; //do nothing
        }
        else if ((target.transform.position.x > transform.position.x) && (facingDirection == -1))//player on right, facing left
        {
            Flip();
        }
        else // player on right, facing right
        {
            return; //do nothing
        }
    }
    protected virtual void RangeCheck()
    {
        isInRangedRange = Physics2D.OverlapCircle(eyeball.position, rangedDistance, playerLayer);
        isInMeleeRange = Physics2D.OverlapCircle(eyeball.position, meleeDistance, playerLayer);
        Locate();

        if (isInMeleeRange && isPlayerDetected)
        {
            StateManager(State.MeleeAttack);
            
        }
        else if (isInRangedRange && isPlayerDetected)
        {
            if (hasRangedAttack == true)
            {
                StateManager(State.RangedAttack);

            }
            
        }
        else
        {
            StateManager(State.Chase);
        }
    }
    protected virtual void MoveRangeCheck()
    {
        float distanceFromTarget = Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x);
        distanceFromTarget = Mathf.Abs(distanceFromTarget);

        if (distanceFromTarget <= 0.5f)  // Within .3 of target
        {
            shouldMove = false;
        }
        else
        {
            shouldMove = true;
        }
    }
    protected virtual void Jump()
    {
        if (isGrounded) //must be on the ground
        {
            verticalVelocity = pawn.JumpHeight;//how high to jump
            rb2d.velocity = new Vector2(rb2d.velocity.x, verticalVelocity);
        }
    }
    protected virtual void Flee()
    {
        Vector2 vectorToTarget = target.transform.position - pawn.transform.position; // Vector from ai to target        
        Vector2 vectorAwayFromTarget = -1 * vectorToTarget; //Reverse vector        
        vectorAwayFromTarget.Normalize(); //Normalize vector      
        vectorAwayFromTarget *= fleeDistance;//set vector length to flee distance
        rb2d.velocity = new Vector2(vectorAwayFromTarget.x * pawn.RunSpeed, rb2d.velocity.y);//run away at runspeed
            
    }
    protected virtual void MeleeAttack()
    {
        RangeCheck();
        eCombat.ECombo1();
    }
    protected virtual void RangedAttack()
    {
        RangeCheck();
        Debug.Log("Ranged Attack Here");
    }

    #endregion
    #region gizmo
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerCheck.position, playerCheckDistance);
    }
    #endregion

    #endregion
}
