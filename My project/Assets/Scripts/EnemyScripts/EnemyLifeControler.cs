using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeControler : MonoBehaviour
{
    public float MaxLife;
    [SerializeField] private float Life;
    [SerializeField] EnemyHealthbar healthbar;
    private void Awake()
    {
        healthbar = GetComponentInChildren<EnemyHealthbar>();
    }
    private void Start()
    {
        Life = MaxLife;
    }
    public void OnDamage(float damage)
    {
        Life -= damage;
        healthbar.UpdateHealthbar(Life,MaxLife);
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
