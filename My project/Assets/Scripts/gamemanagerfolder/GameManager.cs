using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

public class GameManager : MonoBehaviour
{
    [SerializeField] Grid gridlayout;
    [SerializeField] int pointInArray;
    public LayerMask tilemapLayer;
    public GameObject focusGameobject;
    [SerializeField] GameObject[] towerLocation;

    [Header("Money UI")]
    public float currentMoney;
    [SerializeField] public TMP_Text goldInfo;

    [Header("Life UI")]
    public int currentLife;
    [SerializeField] public TMP_Text life;

    [Header("Tower UI")]
    [SerializeField] private Canvas towerInfo;
    [SerializeField] public TMP_Text towerName;
    [SerializeField] public TMP_Text towerAttackSpeed;
    [SerializeField] public TMP_Text towerAttackDamage;

    [Header("Enemie UI")]
    [SerializeField] private Canvas enemieInfo;
    [SerializeField] public TMP_Text enemieName;
    [SerializeField] public TMP_Text enemieMaxHealth;
    [SerializeField] public TMP_Text enemieEndDamage;

    [Header("Purchase tower UI")]
    [SerializeField] private Canvas purchaseTower;

    private void Start()
    {
        currentMoney = 100;
        goldInfo.text = currentMoney.ToString();
        life.text = currentLife.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
                focusGameobject = hit.collider.gameObject;
                if (hit.collider.gameObject.tag == "TowerLocation")
                {
                    focusGameobject = getClosestTowerLocation(hit.point);
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(false);
                    purchaseTower.gameObject.SetActive(true);

                }
                else if (hit.collider.gameObject.tag =="Tower")
                {
                    purchaseTower.gameObject.SetActive(false);
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(true);
                    displayTowerInfo(focusGameobject.name,
                                    focusGameobject.GetComponent<BasicTowerScript>().towerstats.attackSpeed[focusGameobject.GetComponent<BasicTowerScript>().level],
                                    focusGameobject.GetComponent<BasicTowerScript>().towerstats.attackDamage[focusGameobject.GetComponent<BasicTowerScript>().level]);

                }
                else if(hit.collider.gameObject.tag == "Enemy")
                {
                    purchaseTower.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(false);
                    enemieInfo.gameObject.SetActive(true);
                    displayEnemyInfo(focusGameobject.name, focusGameobject.GetComponent<EnemyLifeControler>().enemyScript.maxHealth,
                        focusGameobject.GetComponent<EnemyLifeControler>().Life,
                        focusGameobject.GetComponent<EnemyLifeControler>().enemyScript.reachEndDamage);

                }
                else if(hit.collider.gameObject.tag == "ExplosiveTower")
                {
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(true);
                    displayTowerInfo(focusGameobject.name,
                                    focusGameobject.GetComponent<BasicTowerScript>().towerstats.attackSpeed[focusGameobject.GetComponent<BasicTowerScript>().level],
                                    focusGameobject.GetComponent<BasicTowerScript>().towerstats.attackDamage[focusGameobject.GetComponent<BasicTowerScript>().level]);
                }
                else
                {
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(false);
                    purchaseTower.gameObject.SetActive(false);
                }

            }
        }

    }



    public void displayTowerInfo(string name, float attackSpeed, float damage)
    {
        towerName.text = name;
        towerAttackSpeed.text = attackSpeed.ToString();
        towerAttackDamage.text = damage.ToString();
    }
    public void displayEnemyInfo(string name, float maxHealth, float currentHealth,float damage)
    {
        enemieName.text = name;
        enemieMaxHealth.text = currentHealth + " / " + maxHealth.ToString();
        enemieEndDamage.text = damage.ToString();
    }

    public void createTower(GameObject tower)
    {
        if (spendMoney(tower.GetComponent<BasicTowerScript>().towerstats.upgradeCost[0]))
        {
            GameObject obj = Instantiate(tower, focusGameobject.transform);
            displayTowerInfo(obj.name, obj.GetComponent<BasicTowerScript>().towerstats.attackSpeed[obj.GetComponent<BasicTowerScript>().level],
                                        obj.GetComponent<BasicTowerScript>().towerstats.attackDamage[obj.GetComponent<BasicTowerScript>().level]);
            enemieInfo.gameObject.SetActive(false);
            towerInfo.gameObject.SetActive(true);
            purchaseTower.gameObject.SetActive(false);
        }
        
    }



    public void enemyReachEnd(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            float x = int.Parse(life.text);
            x -= obj.GetComponent<BasicEnemy>().enemyScript.reachEndDamage;
            life.text = x.ToString();
            obj.SetActive(false);
        }
    }

    public bool spendMoney(float amount)
    {
        if (amount > currentMoney)
        {
            return false;
        }
        else
        {
            currentMoney -= amount;
            goldInfo.text = currentMoney.ToString();
            return true;
        }
    }
    public void gainMoney(int amount)
    {
        currentMoney += amount;
        goldInfo.text = currentMoney.ToString();
    }
    public GameObject getClosestTowerLocation(Vector2 givenposition)
    {
        GameObject closestTower = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject trans in towerLocation)
        {
            float dist = Vector3.Distance(trans.transform.position, givenposition);
            if (dist < minDist)
            {
                closestTower = trans;
                minDist = dist;
            }
        }

        return closestTower;
    }
}
