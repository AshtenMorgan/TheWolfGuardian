using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public Health health;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (!health)
        {
            health = GameObject.Find("Ashlynn(Clone)").GetComponent<Health>(); // Only place we can get it to work for now
        }
        else
        {
            healthSlider.value = health.percent;
        }
        
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
