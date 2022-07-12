using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class CombatTest : MonoBehaviour
{
    public static CombatTest PlayerCombatInstance;
    #region Variables
    public Animator anim;
    protected Pawn pawn;
    protected Controller controller;
    protected PlayerController playercontroller; 
    public PlayerInputActions playerLightAttack;
    private InputAction lightPunch;
    public bool isAttacking = false;

    [SerializeField]
    private Transform attackPointIdle;
    [SerializeField]
    private Transform attackPointCrouched;
    [SerializeField]
    private Transform attackPointDownAir;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    #endregion

    #region Functions
    private void Awake()
    {
        PlayerCombatInstance = this;
        playerLightAttack = new PlayerInputActions();
        anim = GetComponent<Animator>();
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
        else if(isAttacking && anim.GetBool("Crouched"))
        {
            CrouchedAttackAOE();
        }
        else if(isAttacking && !anim.GetBool("Grounded"))
        {
            DownAirAttackAOE();
        }
    }

    #region Input Actions
    private void OnEnable()
    {
        lightPunch = playerLightAttack.PlayerHuman.LightPunch;
        playerLightAttack.Enable();
        lightPunch.performed += LightPunch;

    }
    private void OnDisable()
    {
        playerLightAttack.Disable();
    }
    private void LightPunch(InputAction.CallbackContext context)
    {
        //Debug.Log(context);

        if (!isAttacking)
            isAttacking = true;
    }
    #endregion

    #region CombatAOE
    void GroundLightAttackAOE()
    {
            Collider2D[] assetsHitIdle = Physics2D.OverlapCircleAll(attackPointIdle.position, attackRange, enemyLayers);

            foreach(Collider2D Enemy in assetsHitIdle)
            {
                Debug.Log("We hit" + Enemy.name);
            }
    }

    void CrouchedAttackAOE()
    {
        Collider2D[] assetsHitCrouched = Physics2D.OverlapCircleAll(attackPointCrouched.position, attackRange, enemyLayers);

        foreach (Collider2D Enemy in assetsHitCrouched)
        {
            Debug.Log("We hit" + Enemy.name);
        }
    }
    void DownAirAttackAOE()
    {
        Collider2D[] assetsHitDownAir = Physics2D.OverlapCircleAll(attackPointDownAir.position, attackRange, enemyLayers);

        foreach (Collider2D Enemy in assetsHitDownAir)
        {
            Debug.Log("We hit" + Enemy.name);
        }
    }
    #endregion
    #region Gizmos
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

    #endregion
}