using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BasicEnemy : MonoBehaviour
{
    public EnemyScriptableObject enemyScript;
    private NavMeshAgent agent;
    public Transform targetLocation;
    public bool hasDied;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyScript.movementSpeed;
        agent.destination = targetLocation.position;
        hasDied = false;
    }
    public void onSpawn()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyScript.movementSpeed;
        agent.destination = targetLocation.position;
        hasDied = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
