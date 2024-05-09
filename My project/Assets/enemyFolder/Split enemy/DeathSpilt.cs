using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/deathactions/split")]
public class DeathSpilt : Deathactions
{
    public GameObject babies;
    public string babieName;
    public override void onDeathEffect(GameObject obj, GameObject gameManager)
    {
        GameObject bab1 = gameManager.GetComponent<ObjectPooler>().SpawnFromPool(babieName, obj.transform.position, obj.transform.rotation);
        bab1.GetComponent<BasicEnemy>().onSpawn(obj, obj.GetComponent<BasicEnemy>().targetLocation,"basic");
        GameObject bab2 = gameManager.GetComponent<ObjectPooler>().SpawnFromPool(babieName, obj.transform.position, obj.transform.rotation);
        bab2.GetComponent<BasicEnemy>().onSpawn(obj, obj.GetComponent<BasicEnemy>().targetLocation, "basic");
    }
}
