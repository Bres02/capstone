using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask tilemapLayer;
    [SerializeField] GameObject[] towerLocation;
    [SerializeField] GameObject tower;
    
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
                Debug.Log(getClosestTowerLocation(hit.point).transform.position);
                if (hit.collider.gameObject.tag == "TowerLocation")
                {
                    getClosestTowerLocation(hit.point).GetComponent<Testspawner1>().createTower();                    
                }
                
            }
        }

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
