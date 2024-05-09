using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using Unity.VisualScripting;
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
    public void onSpawn(GameObject startposition, Transform target, string path)
    {
        targetLocation = target;
        agent = GetComponent<NavMeshAgent>();
        agent.agentTypeID = GameObject.FindWithTag(path).GetComponents<NavMeshSurface>()[0].agentTypeID;
        agent.speed = enemyScript.movementSpeed;
        agent.destination = targetLocation.position;
        
        hasDied = false;
        transform.position = startposition.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
