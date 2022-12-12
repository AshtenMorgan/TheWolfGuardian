using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRock : Projectile
{
    //[Serialized Field]
    //public audio clip whatever the sound is

    private void OnEnable()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter(Collider col)
    {
        //AudioSource.PlayClipAtPoint(the sound, transform.position);
        if(col.gameObject.CompareTag("Player"))
        {
            //damage
        }
        //Destroy(gameObject);
    }
}
