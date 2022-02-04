using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    public float pct;
    [SerializeField]
    private Health health;
    [SerializeField]
    private Pawn pawn;
    // Start is called before the first frame update
    private void Start()
    {
        health = transform.root.GetComponent<Health>();
    }

    // Update is called once per frame
    private void Update()
    {
    }


    private void LateUpdate()
    {
        if (health)//if there is a health component
        {
            pct = health.currentHealth / health.maxHealth;//get a number
            bar.fillAmount = pct;//set fill ammount for bar
        }
    }
}