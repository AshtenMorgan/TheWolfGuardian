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
    private Transform attackPoint;
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
        if (isAttacking)
        {
            GroundLightAttackAOE();
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
            Collider2D[] assetsHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D Enemy in assetsHit)
            {
                Debug.Log("We hit" + Enemy.name);
            }
    }
    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion

    #endregion
}