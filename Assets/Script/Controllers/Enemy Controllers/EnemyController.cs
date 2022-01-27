using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : Controller
{
    //vars to be visable for testing purposes only.
    [Header("Enemy Variables")]
    [SerializeField]
    PlayerPawn player;
    [SerializeField]
    EnemyPawn enemy;

    // Start is called before the first frame update
    protected override void Start()
    {
        enemy = GetComponent<EnemyPawn>();//reference this objects pawn
       
        base.Start();//call parents start function
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//calls whatever is in parents update function
    }
    protected override void FixedUpdate()
    {
        player = GameManager.instance.player;//get player from GameManager
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemy.WalkSpeed * Time.deltaTime);//move towards player by "speed" units per second
        base.FixedUpdate();//call parent function   
    }
}
