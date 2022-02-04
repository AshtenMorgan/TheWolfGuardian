using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public Health health;

    private void Awake()
    {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    public void SetHealth()
    {
        healthSlider.value = health.percent;
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
