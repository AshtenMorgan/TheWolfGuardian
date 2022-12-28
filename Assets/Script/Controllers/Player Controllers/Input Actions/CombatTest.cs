using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class CombatTest : MonoBehaviour
{
    public Animator anim; //get access to animator system
    protected PlayerPawn pawn; //get access to player pawn
    public bool isAttacking = false; //initial setting of isAttacking WARNING there is also an isAttacking in animator

    
    public PlayerInputActions playerLightAttack;

    /*Attempt to unsubscribe*/
    //public PlayerInputActions StopMovement;

    private InputAction lightPunch;

    /*Attempt to unsubscribe*/
    //private InputAction stopMove;

    public static CombatTest PlayerCombatInstance;

    [SerializeField]
    private Transform attackPointIdle;
    [SerializeField]
    private Transform attackPointCrouched;
    [SerializeField]
    private Transform attackPointDownAir;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    #region Hitboxes
    public LayerMask enemyLayer;//define player's layer
    /*  2 variables each hitbox (frame)
     *  var 1 defines the game object, var 2 is for the size
     */
    [SerializeField, Tooltip("the first position of Hit A's hitbox.")]
    protected Transform hitAPos0;
    [SerializeField, Tooltip("the first size of hit A's hitbox")]
    protected Vector3 hitAVector0;
    [SerializeField, Tooltip("the first position of Jump Hit A's hitbox")]
    protected Transform hitAJumpPos;
    [SerializeField, Tooltip("the first size of Jump Hit A's hitbox")]
    private Vector3 hitAJumpVector;
    [SerializeField, Tooltip("the first position of Crouch A's hitbox")]
    private Transform hitACrouchPos;
    [SerializeField, Tooltip("the first size of Crouch A's hitbox")]
    private Vector3 hitACrouchVector;
    #endregion

    private void Awake()
    {
        //Moved this section to onEnable()
        //PlayerCombatInstance = this;
        //playerLightAttack = new PlayerInputActions();
        //anim = GetComponent<Animator>();
        //pawn = GetComponent<PlayerPawn>();
        //#region HitBoxSetup
        //hitAPos0 = transform.GetChild(0);//Get the 2nd child object on player pawn
        //#endregion
    }

    private void OnEnable()
    {
        PlayerCombatInstance = this;
        playerLightAttack = new PlayerInputActions();
        anim = GetComponent<Animator>();
        pawn = GetComponent<PlayerPawn>();
        #region HitBoxSetup
        hitAPos0 = transform.GetChild(0).GetChild(0);//Do not re-organize Ashlynn prefab without editing these lines
        hitAJumpPos = transform.GetChild(0).GetChild(1); //First GetChild is HitBoxes in Prefab
        hitACrouchPos = transform.GetChild(0).GetChild(2); //Second GetChild(grandchild) is specific hitbox
        #endregion


        lightPunch = playerLightAttack.PlayerHuman.LightPunch;
        playerLightAttack.Enable();
        lightPunch.performed += LightPunch;




        /*Attempt to unsubscribe*/
        //stopMove = StopMovement.PlayerHuman.Move;
        //StopMovement.Enable();
        //stopMove.performed -= UnSubMove;
    }
    private void OnDisable()
    {
        playerLightAttack.Disable();

        /*Attempt to unsubscribe*/
        //StopMovement.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && anim.GetBool("Grounded"))
        {
            GroundLightAttackAOE();
        }
        else if (isAttacking && anim.GetBool("Crouched"))
        {
            CrouchedAttackAOE();
        }
        else if (isAttacking && !anim.GetBool("Grounded"))
        {
            DownAirAttackAOE();
        }
    }

    private void LightPunch(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        
        if (!isAttacking)
            isAttacking = true;
    }

    /*Attempt to unsubscribe*/
    //private void UnSubMove(InputAction.CallbackContext context)
    //{

    //}

    /*
     *  Damage attempt 1 
     *  Enables a raycast box based on animation frame
     *  This can be cleaned up at a later date
     * 
     */
    public void DamageOverlap(int f)//define f on animation frame event (int)
    {
        //TODO: Move the overlap box with the animation, need to include a check for weather or not damage has occured
        //during this button press.
        switch (f)
        {
            case 0:
                //create a circle and return all the colliders within the area into an array
                Collider2D[] enemiesToDamage0 = Physics2D.OverlapBoxAll(hitAPos0.position, hitAVector0, 0, enemyLayer);
                //for every collider in that array
                if (enemiesToDamage0.Length > 0)
                {
                    for (int i = 0; i < enemiesToDamage0.Length; i++)
                    {
                        enemiesToDamage0[i].GetComponent<Health>().Damage(pawn.DamageA);
                    }
                }
                

                break;
            case 1:
                //create a circle and return all the colliders within the area into an array
                Collider2D[] enemiesToDamage1 = Physics2D.OverlapBoxAll(hitAPos0.position, hitAVector0, 0, enemyLayer);
                //for every collider in that array
                for (int i = 0; i < enemiesToDamage1.Length; i++)
                {
                    enemiesToDamage1[i].GetComponent<Health>().Damage(pawn.DamageA);
                }
                break;
        }

    }

    #region CombatAOE
    void GroundLightAttackAOE()
    {
        Collider2D[] assetsHitIdle = Physics2D.OverlapCircleAll(attackPointIdle.position, attackRange, enemyLayers);

        foreach (Collider2D Enemy in assetsHitIdle)
        {
        }
    }

    void CrouchedAttackAOE()
    {
        Collider2D[] assetsHitCrouched = Physics2D.OverlapCircleAll(attackPointCrouched.position, attackRange, enemyLayers);

        foreach (Collider2D Enemy in assetsHitCrouched)
        {
        }
    }
    void DownAirAttackAOE()
    {
        Collider2D[] assetsHitDownAir = Physics2D.OverlapCircleAll(attackPointDownAir.position, attackRange, enemyLayers);

        foreach (Collider2D Enemy in assetsHitDownAir)
        {
        }
    }
    
  

    private void OnDrawGizmos()
    {
        if (attackPointIdle == null)
            return;
        if (attackPointCrouched == null)
            return;
        if (attackPointDownAir == null)
            return;
        if (isAttacking && !anim.GetBool("Crouched") && anim.GetBool("Grounded"))
        {
            Gizmos.DrawWireSphere(attackPointIdle.position, attackRange);
        }
        if (isAttacking && anim.GetBool("Crouched"))
        {
            Gizmos.DrawWireSphere(attackPointCrouched.position, attackRange);
        }
        if (isAttacking && !anim.GetBool("Grounded"))
        {
            Gizmos.DrawWireSphere(attackPointDownAir.position, attackRange);
        }

    }
    #endregion
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //makes Gizmo for Hitboxes red
        #region Hitbox A Gizmos
        Gizmos.DrawWireCube(attackPointIdle.position, hitAVector0); //displays the size and shape of hitbox
        Gizmos.DrawWireCube(attackPointDownAir.position, hitAJumpVector); //displays the size and shape of hitbox A in the air
        Gizmos.DrawWireCube(attackPointCrouched.position, hitACrouchVector); //displays the size and shape of hitbox A while crouching
        #endregion
    }
}
