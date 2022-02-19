using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.Instance.Player.GetComponent<Health>().Damage(10);
            Debug.Log("Health smack");
        } 
    }
}
