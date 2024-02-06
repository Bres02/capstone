using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicTowerScript : MonoBehaviour
{
    [Header("view range")]
    public float viewRadius = 5f;
    public bool enemiesInRange;

    [Header("targeting")]
    public Collider2D[] rangeCheck;
    public GameObject[] enemieRef;
    public LayerMask targetMask;

    [Header("shooting")]
    public GameObject bullet;
    public int damage = 5; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
        StartCoroutine(shotCooldown());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void shoot()
    {
        GameObject bulletGo = (GameObject) Instantiate(bullet, this.transform.position, this.transform.rotation);
        Bullet bulletScript = bulletGo.GetComponent<Bullet>();
        if (bulletScript != null )
        {
            bulletScript.Seek(enemieRef[enemieRef.Length-1].transform, damage);
        }
    }
    private IEnumerator shotCooldown()
    {
        float delay = 1f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            if(enemieRef.Length!= 0)
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
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
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
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, viewRadius);
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
    //Method called above to calculate the angles of sight, place here to make format easier to read
    private Vector2 DirectionFromviewAngle(float eulerY, float viewAngleDegrees)
    {
        viewAngleDegrees += eulerY;
        return new Vector2(Mathf.Sin(viewAngleDegrees * Mathf.Deg2Rad), Mathf.Cos(viewAngleDegrees * Mathf.Deg2Rad));
    }
}
