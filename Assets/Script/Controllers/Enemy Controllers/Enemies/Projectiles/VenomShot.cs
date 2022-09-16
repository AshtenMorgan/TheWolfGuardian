using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomShot : Projectile
{
    [SerializeField]
    public AudioClip venomSplat;

    private void OnEnable()
    {
        Destroy(gameObject, lifespan);//destroy projectile after lifespan
    }


    void OnTriggerEnter(Collider col) //collisions
    {
        AudioSource.PlayClipAtPoint(venomSplat, transform.position);
        if (col.gameObject.CompareTag("Player"))//If projectile hits player
        {
            //damage   
        }

        //Destroy(gameObject);
    }
}
