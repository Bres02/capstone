using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class changeagenttype : MonoBehaviour
{
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(agent.agentTypeID);
            agent.agentTypeID = 1;
            Debug.Log(agent.agentTypeID);
        }
    }
}
