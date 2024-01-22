using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask tilemapLayer;

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
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, tilemapLayer);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "TowerLocation")
                {
                    Debug.Log("happy");
                }
                else if (hit.collider.gameObject.tag == "Grass")
                {
                    Debug.Log("sad");
                }

            }
        }

    }
}
