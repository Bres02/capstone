using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float movementSpeed;
    public float reachEndDamage;
    public float attackDamage;
    public float attackSpeed;
}
