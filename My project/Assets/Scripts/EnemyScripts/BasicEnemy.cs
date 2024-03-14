using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BasicEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public bool showPath;
    public bool showAhead;
    public Transform targetLocation;

    public float MaxLife;
    [SerializeField] private float Life;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetLocation.position;
        Life = MaxLife;
    }
    public void OnDamage(float damage)
    {
        Life -= damage;
        UpdateHealthbar(Life, MaxLife);
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
