using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
[CreateAssetMenu(menuName = "ScriptableObjects/Hitactions/BulletExplosions")]
public class BulletExplosion : HitActions
{
    public override void onHit(GameObject obj, GameObject target)
    {
        target.gameObject.GetComponent<EnemyLifeControler>().OnDamage(obj.GetComponent<Bullet>().damage);
        Collider2D[] explosionColliders = Physics2D.OverlapCircleAll(obj.transform.position, obj.GetComponent<Bullet>().aoerange);
        foreach (Collider2D colider in explosionColliders)
        {
            if (colider.gameObject.tag == "Enemy" && colider.gameObject.GetInstanceID() != target.GetInstanceID())
            {
                colider.gameObject.GetComponent<EnemyLifeControler>().OnDamage(obj.GetComponent<Bullet>().damage/2f);
            }
        }
    }
}
