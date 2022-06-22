using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class CombatTest : MonoBehaviour
{
    public Animator anim;
    protected Pawn pawn;
    public bool isAttacking = false;
    
    public PlayerInputActions playerLightAttack;

    /*Attempt to unsubscribe*/
    //public PlayerInputActions StopMovement;

    private InputAction lightPunch;

    /*Attempt to unsubscribe*/
    //private InputAction stopMove;

    public static CombatTest PlayerCombatInstance;

    private void Awake()
    {
        PlayerCombatInstance = this;
        playerLightAttack = new PlayerInputActions();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
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

    }

    private void LightPunch(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        
        if (!isAttacking)
        {
            isAttacking = true;
        }
    }

    /*Attempt to unsubscribe*/
    //private void UnSubMove(InputAction.CallbackContext context)
    //{

    //}

}