using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/deathactions/split")]
public class DeathSpilt : Deathactions
{
    public GameObject babies;
    public override void onDeathEffect(GameObject obj)
    {
        Instantiate(babies);
        
        Instantiate(babies);
    }
}