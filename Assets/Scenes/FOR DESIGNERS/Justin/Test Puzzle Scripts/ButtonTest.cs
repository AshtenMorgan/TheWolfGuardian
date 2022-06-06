using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.P))
        {
            //
            Debug.Log("Pressed the box");
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.black;


           
        }
        
    }
}
