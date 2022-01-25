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
    public virtual void OnTriggerEnter(Collider col)
    {
        PlayerPawn player = col.GetComponent<PlayerPawn>();//get colliding player pawn
        if (player)//if player pawn is found
        {
            OnPickup(player);//run OnPickup
        }
    }

    public virtual void OnPickup(PlayerPawn player)
    {
        Destroy(gameObject);//remove this object
    }
}