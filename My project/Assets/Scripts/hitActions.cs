using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitActions: ScriptableObject
{
    public abstract void onHit(GameObject obj, GameObject tar);
}
