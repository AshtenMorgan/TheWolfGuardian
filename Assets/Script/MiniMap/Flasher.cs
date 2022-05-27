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
    protected GameManager gm;
    SpriteRenderer sprite;
    Camera cam;
    BoxCollider2D mapSquare;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!hasEntered)
            sprite.color = unEntered;
        cam = GameObject.FindGameObjectWithTag("MapCam").GetComponent<Camera>();
        mapSquare = GetComponent<BoxCollider2D>();
        gm = GameManager.Instance;
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasEntered = true;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
            gm.currentMap = mapSquare;
            sprite.color = entered;
        }
    }
    
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gm.currentMap == mapSquare)
              InvokeRepeating(nameof(Flashing), 0.0f, 0.1f);
    }
    public virtual  void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke();
            sprite.color = entered;
        }
    }
    public void Flashing()
    {
        if (sprite.color == flashA)
            sprite.color = flashB;
        else
            sprite.color = flashA;
    }
}
