using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    bool hasEntered = false;
    SpriteRenderer sprite;
    Camera cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<PlayerPawn>())
        {
            hasEntered = true;
            cam = GameObject.FindGameObjectWithTag("MapCam").GetComponent<Camera>();
            cam.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, -10.0f);
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.green;
            //StopAllCoroutines();
            //StartCoroutine(Flashy());
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerPawn>())
        {
            if (sprite.color == Color.red)
                sprite.color = Color.green;
            else
                sprite.color = Color.red;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPawn>())
        {
            //StopAllCoroutines();
            sprite.color = Color.green;
        }
    }
    IEnumerator Flashy()
    {
        float intermissionDelay = 0.2f;
        WaitForSeconds waitTime = new WaitForSeconds(intermissionDelay);
        while (true)
        {
            if (sprite.color == Color.red)
                sprite.color = Color.green;
            else
                sprite.color = Color.red;
            yield return waitTime;
        }
    }
}
