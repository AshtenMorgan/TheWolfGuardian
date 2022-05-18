using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private Pawn pawn;
    public List<PowerUp> powerups;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
        powerups = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        //new list to hold our expired powerups
        List<PowerUp> expiredPowerups = new List<PowerUp>();

        //for every powerup in the list
        foreach (PowerUp power in powerups) 
        {
            //decrement duration
            power.duration -= Time.deltaTime;
            //if time is up, deactive the powerup and remove from list
            if (power.duration <= 0) 
            {
                //add to expired list
                expiredPowerups.Add(power);
                //reset the duration on the power for the next add
                power.duration = power.durationReset;
            }
        }

        foreach (PowerUp power in expiredPowerups) 
        {
            //deactivate powerup
            power.OnDeactivate(pawn);
            //remove from powerups list
            powerups.Remove(power);
        }
        //clear list just to be thorough and learn stuff
        expiredPowerups.Clear();
    }

    public void Add(PowerUp powerup) 
    {
        //run OnActivate of powerup
        powerup.OnActivate(pawn);

        //add it to the list if NOT permanent
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup); 
        }
    }
}
