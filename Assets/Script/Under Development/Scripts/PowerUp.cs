using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUp
{
    //MAKE THIS SHIT WURK
    //modifiers
    public float speedModifier = 1.0f;
    public float healthModifier;
    public float maxHealthModifier = 1.0f;
    public float fireRateModifier;
    public float damageModifier;
    public bool isPermanent;
    //powerup duration
    public float duration;
    public float durationReset;

    //increase stats by stat modifier
    //fire rate is subtracted because we are decreasing the delay between each shot
    public void OnActivate(Pawn target) 
    {
        //target.moveSpeed *= speedModifier;
        //target.health += healthModifier;
        //target.maxHealth *= maxHealthModifier;
        //target.shotTimerDelay -= fireRateModifier;
        //target.damage *= damageModifier;
    }

    //decrease stats to original values
    public void OnDeactivate(Pawn target) 
    {
        //target.moveSpeed /= speedModifier;
        //target.health -= healthModifier;
        //target.maxHealth /= maxHealthModifier;
        //target.shotTimerDelay += fireRateModifier;
        //target.damage /= damageModifier;
    }
}
