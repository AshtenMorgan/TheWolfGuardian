using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //TODO: MAKE THIS SHIT WURK
    public PowerUp powerup; //variable to hold the powerup that this pick up has
    public AudioClip feedback; //to hold our audio feedback when a pickup is picked up
    public Transform tf; //for pickup's position

    // Start is called before the first frame update
    void Awake()
    {
        tf = GetComponent<Transform>();
    }

    public void OnTriggerEnter(Collider other) 
    {
        //store object's PowerUpManager if it has one
        PowerUpManager powMan = other.GetComponent<PowerUpManager>();

        //if the other object has a PowerUpManager
        if (powMan != null) 
        {
            //Add the PowerUp
            powMan.Add(powerup);

            if (feedback != null) 
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }
            
        }

        //decrement the number of powerups in the game
        //GameManager.instance.currentPowerUps--;
        //Destroy this pickup
        Destroy(gameObject);
    }
}
