/*
 * Data object for Melee Attack State
 * This holds variables used
 * while entity is in the return state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMeleeAttackStateData", menuName = "Data/State Data/MeleeAttackState Data")]
public class Data_MeleeAttackState : ScriptableObject
{
    public float damage = 5.0f;
    public float attackCooldown = 2.0f;

    //[Header("Prefab Projectiles")]    no need for projectiles as this is a Melee attack, but commenting for effects
    //public GameObject projectilePrefabA;
    //public GameObject projectilePrefabB;
}
