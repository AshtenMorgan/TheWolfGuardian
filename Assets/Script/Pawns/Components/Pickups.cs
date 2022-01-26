using UnityEngine;

public abstract class Pickups : MonoBehaviour
{

    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }

    //when we enter a collision
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPawn player = col.GetComponent<PlayerPawn>();//get colliding player pawn
        if (player)//if player pawn is found
        {
            OnPickup(player);//run OnPickup
        }
    }

    public virtual void OnPickup(PlayerPawn player)//what happens when player hits pickup
    {
        //Destroy(gameObject);//remove this object (Possibly changed to disable once pickups are actually implemented)
    }
}