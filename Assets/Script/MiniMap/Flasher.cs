using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public Color unEntered,
        entered,
        flashA,
        flashB;
    bool hasEntered = false,
        isOverlapping = false;
    SpriteRenderer sprite;
    Camera cam;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!hasEntered)
            sprite.color = unEntered;
        cam = GameObject.FindGameObjectWithTag("MapCam").GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PlayerFlasher"))
        {
            hasEntered = true;
            //isOverlapping = true;
            cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10.0f);
            sprite.color = entered;

            //StopAllCoroutines();
            //StartCoroutine(Flashy());
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFlasher"))
        {
            if (sprite.color == flashA)
                sprite.color = flashB;
            else
                sprite.color = flashA;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFlasher"))
        {
            //StopAllCoroutines();
            //isOverlapping = false;
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
