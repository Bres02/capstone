using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerScript : MonoBehaviour
{
    [Range(1, 360)] public float viewAngle = 45f;
    public float viewRadius = 5f;
    public bool enemiesInRange;
    public GameObject[] playerRef;
    public LayerMask targetMask;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
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
    // Enemy Line of Sight
    private void FieldOfViewCheck()
    {
        
        //Creats an array for objects in the target layer that are within the viewable radius of the object
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        playerRef = new GameObject[rangeCheck.Length];
        int i = 0;
        foreach (Collider2D collider in rangeCheck)
        {
            playerRef[i] = collider.gameObject;
            i++;
        }
        //If the array's length isn't 0, than an object in the target layer is in the radius
        if (rangeCheck.Length != 0)
        {
            //Checks the direction and angle target is in relation to object
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            //Sees if the target is within viewable area, if not it doesn't see target
            if (Vector2.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                enemiesInRange = true;
            }
            else
            {
                enemiesInRange = false;
            }
        }
        else if (enemiesInRange)
        {
            enemiesInRange = false;
        }
    }    
    //Shows the objects viewable radius and the cone of sight for debug purposes
    private void OnDrawGizmos()
    {
        //Shows the radius as a white circle
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, viewRadius);

        //Shows the cone of view as two green lines
        Vector3 viewAngle1 = DirectionFromviewAngle(-transform.eulerAngles.z, -viewAngle * 0.5f);
        Vector3 viewAngle2 = DirectionFromviewAngle(-transform.eulerAngles.z, viewAngle * 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle1 * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle2 * viewRadius);

        //If the object can see player, it draws a red line towards player
        if (enemiesInRange)
        {
            Gizmos.color = Color.red;
            foreach (GameObject x in playerRef)
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
