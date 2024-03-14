using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Grid gridlayout;
    [SerializeField] int pointInArray;
    public LayerMask tilemapLayer;
    
    [SerializeField] GameObject[] towerLocation;

    [Header("Tower UI")]
    [SerializeField] private Canvas towerInfo;
    [SerializeField] public TMP_Text towerName;
    [SerializeField] public TMP_Text towerAttackSpeed;
    [SerializeField] public TMP_Text towerAttackDamage;

    [Header("Enemie UI")]
    [SerializeField] private Canvas enemieInfo;
    [SerializeField] public TMP_Text enemieName;
    [SerializeField] public TMP_Text enemieAttackSpeed;
    [SerializeField] public TMP_Text enemieAttackDamage;
    // Start is called before the first frame update
    void Start()
    {

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
                if (hit.collider.gameObject.tag == "TowerLocation")
                {
                    //getClosestTowerLocation(hit.point).GetComponent<Testspawner1>().createTower();
                    Vector3Int cellPosition = gridlayout.WorldToCell(hit.point);
                    Vector3 cellCenter = gridlayout.GetCellCenterWorld(cellPosition);
                    towerLocation[pointInArray].transform.position = cellCenter;
                }else if (hit.collider.gameObject.tag =="Tower")
                {
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(true);
                    displayTowerInfo(hit.collider.gameObject.name, 
                        hit.collider.gameObject.GetComponent<BasicTowerScript>().towerstats.attackSpeed, 
                        hit.collider.gameObject.GetComponent<BasicTowerScript>().towerstats.attackDamage);

                }else if(hit.collider.gameObject.tag == "Enemy")
                {
                    towerInfo.gameObject.SetActive(false);
                    enemieInfo.gameObject.SetActive(true);
                   
                }
                else if(hit.collider.gameObject.tag == "ExplosiveTower")
                {
                    enemieInfo.gameObject.SetActive(false);
                    towerInfo.gameObject.SetActive(true);
                    displayTowerInfo(hit.collider.gameObject.name,
                        hit.collider.gameObject.GetComponent<ExplosiveTowerScript>().towerstats.attackSpeed,
                        hit.collider.gameObject.GetComponent<ExplosiveTowerScript>().towerstats.attackDamage);
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
        
    }
    public GameObject getClosestTowerLocation(Vector3 givenposition)
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
