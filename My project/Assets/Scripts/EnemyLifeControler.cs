using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeControler : MonoBehaviour
{
    public int Life;

    public void OnDamage(int damage)
    {
        Debug.Log("ouch");
        Life -= damage;
        if (Life <= 0)
        {
            Destroy(gameObject);
            Debug.Log("im dead");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
