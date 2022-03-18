//simple senses

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCheck : MonoBehaviour
{
    private EnemyController controller;

    private void Awake()
    {
        controller = GetComponentInParent<EnemyController>();
    }
    private void OnTriggerEnter2D(Collider2D thingHit)
    {
        if (thingHit.gameObject.tag == "Player")//check to see if thing hit was player
        {
            controller.isPlayerDetected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D thingHit)
    {
        if (thingHit.gameObject.tag == "Player")
        {
            controller.isPlayerDetected = false;
        }
        
    }
}
