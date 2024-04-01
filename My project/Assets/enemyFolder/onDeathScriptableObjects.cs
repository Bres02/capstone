using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/deathactions")]
public abstract class onDeathScriptableobjects : ScriptableObject
{
    public abstract void onDeathEffect(GameObject obj);
}
