using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExplosiveTowerScript : MonoBehaviour
{
    [SerializeField] public TowerScriptableObject towerstats;
    public int level = 0;
    public bool enemiesInRange;
    public Collider2D[] rangeCheck;
    public GameObject[] enemieRef;
    public LayerMask targetMask;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
        StartCoroutine(shotCooldown());

    }
    void shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(towerstats.bullet, this.transform.position, this.transform.rotation);
        ExplosiveBullet bulletScript = bulletGo.GetComponent<ExplosiveBullet>(); 
        if (bulletScript != null)
        {
            if (enemieRef[enemieRef.Length - 1] == null && enemieRef.Length >= 2)
            {
                bulletScript.ExplosionSeek(enemieRef[enemieRef.Length - 2].transform, towerstats.attackDamage[level]);
            }
            else
            {
                bulletScript.ExplosionSeek(enemieRef[enemieRef.Length - 1].transform, towerstats.attackDamage[level]);
            } 
        }
    }
    private IEnumerator shotCooldown()
    {
        float delay = towerstats.attackSpeed[level];
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            if (enemieRef.Length != 0)
            {
                shoot();
            }
        }
    }
    //Has the enemy run field of view every 0.2 seconds instead of every frame
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    public void targeableEnemies()
    {
        rangeCheck = new Collider2D[0];
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, towerstats.Range, targetMask);
        enemieRef = new GameObject[rangeCheck.Length];
        int i = 0;
        foreach (Collider2D collider in rangeCheck)
        {
            enemieRef[i] = collider.gameObject;
            i++;
        }
    }
    // Enemy Line of Sight
    private void FieldOfViewCheck()
    {
        //Creats an array for objects in the target layer that are within the viewable radius of the object
        targeableEnemies();
        //If the array's length isn't 0, than an object in the target layer is in the radius
        if (rangeCheck.Length != 0)
        {
            //Checks the direction and angle target is in relation to object
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            enemiesInRange = true;

        }
        else if (enemiesInRange)
        {
            enemiesInRange = false;
        }
    }
    //Shows the objects viewable radius and the cone of sight for debug purposes
    private void OnDrawGizmos()
    {
        targeableEnemies();
        //Shows the radius as a white circle
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, towerstats.Range);
        //If the object can see player, it draws a red line towards player
        if (enemiesInRange)
        {
            Gizmos.color = Color.red;
            foreach (GameObject x in enemieRef)
            {
                Gizmos.DrawLine(transform.position, x.transform.position);
            }
        }

    }
}
