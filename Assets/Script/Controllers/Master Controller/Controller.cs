using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    #region Variables
    #region General Variables
    protected Combat combat; //stores the combat script for the pawn
    #endregion

    #region Jump Variables
    [Header("Jump Variables")]
    protected float verticalVelocity; //the vertical acceleration of the player pawn
    protected bool isCrouching; //determines if the pawn is crouching
    #endregion

    #endregion


    #region Animator Variables
    [Header("Animator Variables")]
    public Animator ani;//animator code
    #endregion

    #region Combat Variables
    [SerializeField] InputRecorder inputRecorder; //assigns the input recorder script so the controller can properly do combos
    //MovesManager movesManager;
    int CurrentComboPriority = 0;
    int ComboPriority;
    [SerializeField]
    Moveset move; //stores the Moveset enum
    #region Full Properties

    public bool IsCrouching
    {
        get { return isCrouching; }
    }

    #endregion
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        //if (inputRecorder == null)
        //    inputRecorder = FindObjectOfType<InputRecorder>();
        //if (movesManager == null)
        //    movesManager = FindObjectOfType<MovesManager>();

        combat = GetComponent<Combat>();

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        //PlayMove(move, ComboPriority);
    }
    protected virtual void FixedUpdate()
    {

    }

    #region Slope
    protected virtual void SlopeCheck()
    {

    }
    #endregion

    protected virtual void ApplyMovement()
    {
    }

    #region Combat Functions
    public void PlayMove(Moveset move, int ComboPriority) //Get the Move and the Priorty
    {
        if (Moveset.None != move) //if the move is none ignore the function
        {
            if (ComboPriority >= CurrentComboPriority) //if the new move is higher Priorty play it and ignore everything else
            {
                CurrentComboPriority = ComboPriority; //Set the new Combo
                ResetTriggers(); //Reset All Animation Triggers
                inputRecorder.ResetCombo(); //Reset the List in the ControlsManager
            }
            else
                return;

            //Set the Animation Triggers
            switch (move)
            {
                case Moveset.HitAS0:
                    ani.SetTrigger("HitAS0");
                    break;
                case Moveset.HitAS1:
                    ani.SetTrigger("HitAS1");
                    break;
                case Moveset.HitAS2:
                    ani.SetTrigger("HitAS2");
                    break;
                case Moveset.HitAS3:
                    ani.SetTrigger("HitAS3");
                    break;
                case Moveset.HitAA0:
                    ani.SetTrigger("HitAA0");
                    break;
                case Moveset.HitAC0:
                    ani.SetTrigger("HitAC0");
                    break;
                case Moveset.HitCS0:
                    ani.SetTrigger("HitCS0");
                    break;
            }

            CurrentComboPriority = 0; //Reset the Combo Priorty
        }
    }
    void ResetTriggers() //Reset All the Animation Triggers so we don't have overlapping animations
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
        {
            ani.ResetTrigger(parameter.name);
        }
    }
    #endregion





    #endregion
}
