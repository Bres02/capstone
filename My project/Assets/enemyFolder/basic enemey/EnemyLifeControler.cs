using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeControler : MonoBehaviour
{
    public EnemyScriptableObject enemyScript;
    [SerializeField] public float Life;
    [SerializeField] Slider healthbar;
    private void Awake()
    {
        Life = enemyScript.maxHealth;
    }

    public void OnDamage(float damage)
    {
        Life -= damage;
        UpdateHealthbar(Life, enemyScript.maxHealth);
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        healthbar.value = currentValue / maxValue;
    }
  
}
