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
    private GameObject gameManager;
    private void Awake()
    {
        Life = enemyScript.maxHealth;
        gameManager = GameObject.Find("GameManager");
    }

    public void OnDamage(float damage)
    {
        Life -= damage;
        UpdateHealthbar(Life, enemyScript.maxHealth);
        if (Life <= 0 && !gameObject.GetComponent<BasicEnemy>().hasDied)
        {
            gameObject.GetComponent<BasicEnemy>().hasDied = true;
            die();
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
            for (global::System.Int32 i = 0; i < deathActions.Length; i++)
            {
                if (deathActions[i]!=null)
                {
                    deathActions[i].onDeathEffect(this.gameObject, gameManager);
                }
            }
        }
        gameManager.GetComponent<GameManager>().gainMoney(enemyScript.goldValue);
        gameObject.SetActive(false);
    }
}
