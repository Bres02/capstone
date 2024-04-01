using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeControler : MonoBehaviour
{
    public EnemyScriptableObject enemyScript;
    [SerializeField] public float Life;
    [SerializeField] Slider healthbar;
    public Deathactions[] deathActions;
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
            die();
            Destroy(gameObject);
        }
    }
    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        healthbar.value = currentValue / maxValue;
    }
    public void die()
    {
        if (deathActions.Length > 0)
        {
            for (int i = 0; i < deathActions.Length; i++)
            {
                deathActions[i].onDeathEffect(this.gameObject);
            }
        }
    }
}
