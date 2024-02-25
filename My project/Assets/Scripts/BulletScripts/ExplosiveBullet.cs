using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public int damage;
    public int explosionDamage;
    public float range = 5f;
    public void ExplosionSeek(Transform _target, int _damage)
    {
        if (_target == null)
        {
            Debug.Log("fuck");

        }
        target = _target;
        Debug.Log(target.gameObject.name);
        damage = _damage;
        explosionDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {

            Debug.Log("fuck2");
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
        Debug.Log("fuck3");
        Destroy(gameObject);
    }

}
