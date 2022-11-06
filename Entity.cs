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
    [SerializeField] protected float delay=1.0f;

    #endregion
    #region Variables
    #region slope stuff
    [SerializeField] protected float slopeCheckDistance;
    protected float slopeDownAngle;
    protected float slopeDownAngleOld;
    protected float slopeSideAngle;
    protected Vector2 slopeNormalPerp;
    [SerializeField] protected bool isOnSlope;
    [SerializeField] private CircleCollider2D cc;
    private Vector2 colliderSize;
    [SerializeField]
    private LayerMask whatIsGround;
    private Vector2 newVelocity;


    #endregion
    #region Components
    public StateMachine fsm;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public Health health { get; private set; }
    public Data_Entity entityData;
    public GameObject target { get; private set; }

    private GameObject floatingTextPrefab;
    public GameObject fTp; //call to parent object of floating text, used parent to allow for animation & scrolling
    private Vector3 bump = new Vector3(0, 2, 0.5f);

    #endregion
    #region Movement
    public int facingDirection { get; private set; }
    private Vector2 tempV2;

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
        cc = GetComponent<CircleCollider2D>();
        colliderSize = new Vector2(cc.radius, cc.radius);//circles are weird for x,y examples
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
        
        if (health.isDead==true) //trying to test for death before update, otherwise object could go away before text is deleted
        {
            floatingTextPrefab.GetComponentInChildren<TextMeshPro>().text = transform.position.ToString() + "\n" + fsm.currentState.ToString() + "\n" + health.isDead.ToString(); //Only duplicated to show death state
            Destroy(floatingTextPrefab); //cleanup diag text if monster dies
            Debug.Log("called destructors");
            Destroy(this); //kill the script so we don't have zombie states & entities, also need to kill in inherited versions
        }
        else
        {
            floatingTextPrefab.transform.localPosition = base.transform.position + bump;
            floatingTextPrefab.GetComponentInChildren<TextMeshPro>().text = transform.position.ToString() + "\n" + fsm.currentState.ToString() + "\n" + health.isDead.ToString();
        }
        slopeCheck();
        fsm.currentState.PhysicsUpdate();//call physics update in fixedupdate
    
        
       
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

    public virtual void SetVelocity(float velocity) //this is the equivalent of movement code, needs new vectors for slope
    {
        tempV2.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = tempV2; // regular side to side movement
        
        if(isOnSlope)
        {
            newVelocity.Set(tempV2.x*velocity, tempV2.y *velocity* slopeNormalPerp.y);
            rb.velocity = newVelocity;
        }
      
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
            lagFlip(1.0f);//If it is not facing the target it will flip, and then return the distance.
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
    public virtual void lagFlip(float delay) //this is a slowed version of flip to allow for hitting some enemies on the backside
    {
        Invoke("Flip", delay);
        //Debug.Log("delayed flip called "+delay); It's working now, no need for debug.log
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void slopeCheck()
    {
        Vector2 checkPos = cc.bounds.center - new Vector3(0.0f, colliderSize.y/2);
        slopeCheckVertical(checkPos);

    }
    public virtual void slopeCheckHorizontal(Vector2 checkPos)
    {

    }
    public virtual void slopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }
            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.black);
            Debug.DrawRay(hit.point, hit.normal, Color.blue); //test draw raycast
        }
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