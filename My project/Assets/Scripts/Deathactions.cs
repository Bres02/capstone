using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deathactions : ScriptableObject
{
    public abstract void onDeathEffect(GameObject obj, GameObject gameManager);
}
