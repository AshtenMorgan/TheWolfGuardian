/*
 * Data object for Ranged Attack State
 * This holds variables used
 * while entity is in the return state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewRangedAttackStateData", menuName = "Data/State Data/RangedAttackState Data")]
public class Data_RangedAttackState : ScriptableObject
{
    public float damage = 5.0f;
    public float projectialSpeedA = 20.0f;
    public float projectialSpeedB = 15.0f;
    public float attackCooldown = 2.0f;
  
    [Header ("Prefab Projectiles")]
    public GameObject projectilePrefabA;
    public GameObject projectilePrefabB;
}
