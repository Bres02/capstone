using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class BasicTowerScript : MonoBehaviour
{

    [SerializeField] public TowerScriptableObject towerstats;
    public int level = 0;
    public bool enemiesInRange;
    public Collider2D[] rangeCheck;
    public GameObject[] enemieRef;
    public LayerMask targetMask;
    
    public bool canShoot = false;
    public float cooldownTimer;

    public GameObject gameManager;

    private void Awake()
    {
        FieldOfViewCheck();
        StartCoroutine(FOVRoutine());
        cooldownTimer = towerstats.attackSpeed[level];
        gameManager = GameObject.Find("GameManager");
    }

    void shoot()
    {
        //GameObject bulletGo = (GameObject) Instantiate(towerstats.bullet, this.transform.position, this.transform.rotation);
        GameObject bulletGo = gameManager.GetComponent<ObjectPooler>().SpawnFromPool(towerstats.bullet,this.transform.position, this.transform.rotation);
        Bullet bulletScript = bulletGo.GetComponent<Bullet>();
        if (bulletScript != null && enemieRef.Length >0)
        {
            if (enemieRef[enemieRef.Length - 1] == null && enemieRef.Length >= 2)
            {
                transform.GetChild(level).gameObject.GetComponent<Animator>().SetTrigger("attack");
                bulletScript.Seek(enemieRef[enemieRef.Length - 2].transform, towerstats.attackDamage[level]);
                canShoot = false;
                cooldownTimer = towerstats.attackSpeed[level];
            }
            else
            {
                transform.GetChild(level).gameObject.GetComponent<Animator>().SetTrigger("attack");
                bulletScript.Seek(enemieRef[enemieRef.Length - 1].transform, towerstats.attackDamage[level]);
                canShoot = false;
                cooldownTimer = towerstats.attackSpeed[level];
            }
        }
    }
    private void FixedUpdate()
    {
        if (enemieRef.Length != 0 && canShoot)
        {
            shoot();
        }
        if (!canShoot)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canShoot = true;
            }
        }


    }
    private void Update()
    {
        if (rangeCheck.Length >0)
        {
            if (rangeCheck[0] != null)
            {
                Vector3 offset = rangeCheck[0].transform.position - transform.GetChild(level).transform.position;
                transform.GetChild(level).transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
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


            //transform.GetChild(level).transform
        }
        else if (enemiesInRange)
        {
            enemiesInRange = false;
        }
    }
    public void levelUp()
    {
        transform.GetChild(level).gameObject.SetActive(false);
        level += 1;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = towerstats.levelSprite[level];
        transform.GetChild(level).gameObject.SetActive(true);
    }
    //Shows the objects viewable radius
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
