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
        Attack,
        Hurt,
        Dead
    }
    [SerializeField]
    protected State currentState = State.Patrol;
    #endregion

    #region objects
    [Header("Enemy Variables")]
    [SerializeField]
    PlayerPawn target;
    [SerializeField]
    EnemyPawn pawn;
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
                    playerSeeDistance = 5.0f,
                    harmStart,
                    harmDuration;

    [SerializeField]
    public bool isGroundDetected = true,
                   isWallDetected = false,
                   isEnemyDetected = false,
                   isPlayerDetected = false;

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
        ani = GetComponent<Animator>();//get animator component
        target = GameManager.Instance.Player;//get player from game manager
        facingDirection = 1;//face right
    }

    // Update is called once per frame
    protected override void Update()
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
            case State.Attack:
                UpdateAttackState();
                break;
            case State.Hurt:
                UpdateHurtState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        #endregion
        base.Update();//calls whatever is in parents update function
    }

    protected override void FixedUpdate()
    {
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

    }
    protected virtual void ExitFleeState()
    {

    }
    #endregion

    #region Attack
    protected virtual void EnterAttackState()
    {

    }
    protected virtual void UpdateAttackState()
    {

    }
    protected virtual void ExitAttackState()
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
        switch (currentState)
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
            case State.Attack:
                ExitAttackState();
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
            case State.Attack:
                EnterAttackState();
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
        gameObject.transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        movement.Set(pawn.WalkSpeed * facingDirection, rb2d.velocity.y);//set speed to walk
        rb2d.velocity = movement;//set velocity to movement speed/direction
    }
    protected virtual void StepDetection()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the pawn is grounded
        isGroundDetected = Physics2D.Raycast(eGroundCheck.position, Vector2.down, groundCheckDistance, groundLayer);//raycast for ground
        isWallDetected = Physics2D.Raycast(eyeball.position, transform.right, wallCheckDistance, groundLayer);//raycast for wall
    }
    protected virtual void Walk()
    {
        if (isGrounded)//only do actions while on the ground
        {
            Locate();//look for obstacles
            if (!isGroundDetected || isWallDetected || isEnemyDetected)//there is no ground or there is a wall or enemy
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
        StepDetection();//run checks for continued movement
        Locate();//continue looking for player
        if (isPlayerDetected)
        {
            movement.Set(pawn.RunSpeed * facingDirection, rb2d.velocity.y);//set movement speed to run towards player
            rb2d.velocity = movement;//set velocity to movement speed/direction

            if (!isGroundDetected)
            {
                Jump();//should attempt to jump gaps
            }
        }
        else if (!isPlayerDetected)
        {
            StateManager(State.Patrol);//return to patrol if we cant see player anymore
        }
    }
    protected virtual void Locate()
    {
        Collider2D[] Collider = Physics2D.OverlapBoxAll(playerCheck.position, playerCheckDistance, 0, playerLayer);
        //Collider2D[] Collider = Physics2D.OverlapCollider(VisionCone, playerLayer);
        if (Collider.Length > 0)//overlap hit something
        {
            isPlayerDetected = Physics2D.Raycast(eyeball.position, target.transform.position - eyeball.position, playerSeeDistance, playerLayer);
            Debug.DrawRay(eyeball.position, (target.transform.position - eyeball.position) * playerSeeDistance, Color.red);
        }
        else
        {
            isPlayerDetected = false;
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
