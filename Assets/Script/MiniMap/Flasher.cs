using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public Color unEntered,
        entered,
        flashA,
        flashB;
    bool hasEntered = false;
    SpriteRenderer sprite;
    Camera cam;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!hasEntered)
            sprite.color = unEntered;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<PlayerPawn>())
        {
            hasEntered = true;
            cam = GameObject.FindGameObjectWithTag("MapCam").GetComponent<Camera>();
            cam.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, -10.0f);
            
            sprite.color = entered;
            //StopAllCoroutines();
            //StartCoroutine(Flashy());
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if (collision.GetComponent<PlayerPawn>())
        {
            if (sprite.color == flashA)
                sprite.color = flashB;
            else
                sprite.color = flashA;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPawn>())
        {
           //StopAllCoroutines();
            sprite.color = entered;
        }
    }
    IEnumerator Flashy()
    {
        float intermissionDelay = 0.2f;
        WaitForSeconds waitTime = new WaitForSeconds(intermissionDelay);
        while (true)
        {
            if (sprite.color == flashA)
                sprite.color = flashB;
            else
                sprite.color = flashA;
            yield return waitTime;
            StartCoroutine(Flashy());
        }
    }
}
