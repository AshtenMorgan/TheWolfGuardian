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
        health = GameObject.FindWithTag("Player").GetComponent<Health>(); //assigns the player Health component to the healthbar
    }

    public void Update()
    {
        
        healthSlider.value = health.percent;
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
