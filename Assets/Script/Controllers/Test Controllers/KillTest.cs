using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTest : MonoBehaviour
{
    public float testDamage = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.Instance.player.GetComponent<Health>().Damage(testDamage);
            GameManager.Instance.UpdateHealthBar();
        } 
    }
}
