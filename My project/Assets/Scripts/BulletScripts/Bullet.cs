using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float damage;
    public bool seekrun = false;

    public void Seek(Transform _target, float _damage)
    {
        if (_target == null)
        {

        }
        else
        {
            target = _target;
            damage = _damage;
            seekrun = true;
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

        if(dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        target.gameObject.GetComponent<BasicEnemy>().OnDamage(damage);
        Destroy(gameObject);
    }
}
