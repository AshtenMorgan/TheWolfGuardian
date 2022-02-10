using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public Health health;

    private void Start()
    {
        health = GameManager.Instance.Player.GetComponent<Health>(); //assigns the player Health component to the healthbar
    }

    public void Update()
    {
        healthSlider.value = health.GetPercent();

    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
