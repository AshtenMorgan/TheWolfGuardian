using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField, Tooltip("Current target of enemy")]
    private Rigidbody target;

    private int enemyLayer;//not sure what we wanted this for, maybe meant to be assigned in unity

    [SerializeField, Tooltip("What is the AI currently doing")]
    private int aiState;




    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
