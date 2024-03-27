using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float damage;
    public float explosionDamage;
    public float range = 5f;
    public bool seekrun = false;
    public void ExplosionSeek(Transform _target, float _damage)
    {
        if (_target == null)
        {
    
        }
        else
        {
            seekrun = true;
            target = _target;
            damage = _damage;
            explosionDamage = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && seekrun == true)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        target.gameObject.GetComponent<EnemyLifeControler>().OnDamage(damage);
        Collider2D[] explosionColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D colider in explosionColliders)
        {
            if (colider.gameObject.tag == "Enemy")
            {
                colider.gameObject.GetComponent<EnemyLifeControler>().OnDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }

}
