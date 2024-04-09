using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float damage;
    public bool seekrun = false;

    [Header("For Explosive Tower")]
    public float aoerange;


    public HitActions[] hitActions;

    public void Seek(Transform _target, float _damage)
    {
        if (_target == null)
        {

        }
        else
        {
            target = _target;
            damage = _damage;
        }
        seekrun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null /*&& seekrun == true*/)
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
        target.gameObject.GetComponent<EnemyLifeControler>().OnDamage(damage);
        if (hitActions.Length >= 0)
        {
            for (global::System.Int32 i = 0; i < hitActions.Length; i++)
            {
                hitActions[i].onHit(this.gameObject, target.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
