/*
 * Master Entity Class
 * includes functions for things such as AI sences,
 * enemies components (rigidbody, animator, health, etc)
 * along with the movement and flip functions 
 * 
 */
#region Include
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Threading.Tasks;
#endregion

#region Requirements
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
#endregion

#region Class
public class Entity : MonoBehaviour
{


    #region Variables
    #region Box Raycast

    public bool PlayerDetected;

    //Moved to SpiderChaseState
    //public bool PlayerDetected = false;
    [SerializeField, Header("OverlapBox parameters")]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.one;
    //public Quaternion boxRotation;

    public float detectionDelay = 0.3f;

    [Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;
    [SerializeField] protected float delay = 1.0f;

    #endregion
    #region slope stuff
    [SerializeField] protected float slopeCheckDistance;
    protected float slopeDownAngle;
    protected float slodeDownAngleOld;
    protected float slopeSideAngle;
    protected Vector2 slopeNormalPerp;
    [SerializeField] protected bool isOnSlope;
    #endregion
    #region Components
    public StateMachine fsm;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public Health health { get; private set; }
    public Data_Entity entityData;
    public GameObject target { get; private set; }

    protected GameObject floatingTextPrefab;
    public GameObject fTp;
    protected Vector3 bump = new Vector3(0, 2, 0.5f);

    #endregion
    #region Movement
    public int facingDirection { get; private set; }
    protected Vector2 tempV2;

    [SerializeField]
    protected Transform wallCheck,
        ledgeCheck;
    #endregion
    #region Attacks
    public Transform originAttackA;
    public Transform originAttackB;
    #endregion

    #endregion

    public virtual void Start()
    {
        //get components
        
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        health = GetComponent<Health>();
        target = GameManager.Instance.player.gameObject;
        floatingTextPrefab = Instantiate(fTp);
        fsm = new StateMachine();//Create state machine
    }

    public virtual void OnEnable()
    {
        facingDirection = 1;
    }
    public virtual void Update()
    {
        fsm.currentState.LogicUpdate();//call logic update in update
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();//call physics update in fixedupdate
        floatingTextPrefab.transform.localPosition = base.transform.position+bump;
        floatingTextPrefab.GetComponentInChildren <TextMeshPro>().text = transform.position.ToString()+"\n"+fsm.currentState.ToString();
    }

    //create a box to see if player is near entity
    public bool PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)wallCheck.position + detectorOriginOffset * Vector3.forward, detectorSize, 0, entityData.whatIsPlayer);
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void SetVelocity(float velocity)
    {
        tempV2.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = tempV2;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, wallCheck.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) <= entityData.viewDistance;
    }

    public bool CanSeeTarget()
    {
        if (!LeftRight())
       {
            return Vector2.Distance(transform.position, target.transform.position) <= entityData.viewDistance;//If it is facing towards the target, returns the distance to the target
        }
       else
        {
            LagFlip(1.0f);//If it is not facing the target it will flip, and then return the distance.
            return Vector2.Distance(transform.position, target.transform.position) < entityData.viewDistance;
        }
   
    }
    public virtual bool LeftRight()
    {
        if (target.transform.position.x > transform.position.x && target.transform.position.x - transform.position.x > .5)//target is on right side of entity, should be facing right, plus margin for being on top.
        {
            if (facingDirection == 1)//entity is facing right
            {
                return false;//no flip needed
            }
            else
            {
                return true;//need to flip
            }
        }
        else if (target.transform.position.x < transform.position.x && transform.position.x - target.transform.position.x > .5)//target is on the left (or on top of) entity, plus margin for being on top.
        {
            if (facingDirection == -1)//entity facing left
            {
                return false;//no flip
            }
            else//entity facing right
            {
                return true;//flip
            }
        }
        else
        { 
            return false;//If it is on top, returns false as to not get stuck in an infinite loop. 
        }
    }
    public virtual void LagFlip(float delay)
    {
        Invoke(nameof(Flip), delay);
        Debug.Log("delayed flip called "+delay);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
   
    #region Gizmos
    

    public virtual void OnDrawGizmos()
    {
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(entityData.wallCheckDistance * facingDirection * Vector2.right));
        //Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, target.transform.position);

        //Box Cast visual
        if(showGizmos && wallCheck != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector2)wallCheck.position + detectorOriginOffset, detectorSize);
        }
    }   
    #endregion
}
#endregion