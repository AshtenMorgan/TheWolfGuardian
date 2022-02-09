/*This script contains functionalities for health in a video game
 * Such as, getters and setters for current health, max health, and a percentage value of health
 * It also contains functions for healing, taking damage, and dying
 * 
 * Just add this component to anything you want to be able to die or take damage.
 * Adding a correlating display is easy with the getter and setter functions
 * 
 * It is older than this project would have you believe so I do not remember what the Invoking functions are for
 * this part of the description will remain until I discover it again
 * 
 * James Pope 9/25/21 and Modified by John Pope 2/8/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField, Tooltip("Max health of the player")]
    private float MaxHealth = 100f;
    [SerializeField, Tooltip("Current health of the player")]
    private float currentHealth = 100f;
    private float percent; //percentage of health

    [Header("Events")]
    [SerializeField, Tooltip("Called every time the object is healed.")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Called every time the object is damaged.")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Called once when the object's health reaches 0.")]
    private UnityEvent onDeath;

    /// <summary>
    /// Gets the current health of the player
    /// </summary>
    /// <returns>float</returns>
    public float GetHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Sets the health of the player to the float parameter value
    /// </summary>
    /// <param name="value"></param>
    public void SetHealth(float value)
    {
        currentHealth = value;
    }

    /// <summary>
    /// Gets the max health of the player
    /// </summary>
    /// <returns>float</returns>
    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    /// <summary>
    /// Sets the max health of the player to float parameter value
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxHealth(float value)
    {
        MaxHealth = value;
    }

    /// <summary>
    /// Gets the percentage of the current player's health
    /// </summary>
    /// <returns>float</returns>
    public float GetPercent()
    {
        percent = currentHealth / MaxHealth;
        return percent;
    }

    /// <summary>
    /// Sets the percentage value of the players health. Mostly used to update displays and trigger effects.
    /// </summary>
    private void SetPercent()
    {
        percent = currentHealth / MaxHealth;
    }

    /// <summary>
    /// Restores the player's health by the parameter value, clamped between 0 and max health
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(float heal)
    {
        heal = Mathf.Max(heal, 0f);
        currentHealth = Mathf.Clamp(currentHealth + heal, 0f, MaxHealth);
        SendMessage("onHeal", SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Sets the current health equal to the players max health
    /// </summary>
    public void FullHeal()
    {
        currentHealth = MaxHealth;
    }

    /// <summary>
    /// Subtracts the float value of damage from the players health clamped between 0 and max health
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        damage = Mathf.Max(damage, 0f);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, MaxHealth);
        if (currentHealth <= 0f)
        {
            onDeath.Invoke();
        }
    }

    /// <summary>
    /// Sets the players health to zero, in its current state it is dangerous as it destroys the game object it is attached to.
    /// </summary>
    public void Kill()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

    /// <summary>
    /// This exists to call player death on this object instead of a prefab
    /// </summary>
    public void Die()
    {
        //Object Pull or destroy
        Destroy(gameObject);
    }

    public void Respawn()
    {
    Health healthReset = gameObject.GetComponent<Health>();
    }
}
