using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShot : Projectile
{
    [SerializeField]
    public AudioClip webSplat;

    private void OnEnable()
    {
        Destroy(gameObject, lifespan);//destroy projectile after lifespan
    }


    void OnTriggerEnter(Collider col) //collisions
    {
            AudioSource.PlayClipAtPoint(webSplat, transform.position);
            if (col.gameObject.CompareTag("Player"))//If projectile hits player
            {
                //stun or whatever   
            }

        //Destroy(gameObject);
    }
}
