using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField, Range(0, 100), Tooltip("The amount this pickup heals the entity that picks it up.")] 
    private float healAmount;
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnPickUp(GameObject entity)
    {
        //get the entity that we collided with's health component
        Health entityHealth = entity.GetComponent<Health>();
        if (entity.GetComponent<Health>() != null)
        {
            if (entityHealth.GetHealth() < entityHealth.GetMaxHealth())
            {
                //pass the heal amount to their heal function
                entityHealth.Heal(healAmount);
                Debug.Log("You've been healed!");
                //destroy object
                base.OnPickUp(entity);
            } 
        }
    }
}
