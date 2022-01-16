using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField, Tooltip("Current target of enemy")]
    protected Rigidbody target;

    private GameObject enemyLayer;//not sure what we wanted this for, maybe meant to be assigned in unity

    [SerializeField, Tooltip("What is the AI currently doing")]
    protected int aiState;




    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
