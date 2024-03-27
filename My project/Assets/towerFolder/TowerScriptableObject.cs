using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    public float Range;
    public float[] attackSpeed;
    public float[] attackDamage;
    public float[] upgradeCost;
    public bool towerEffect1;
    public bool towerEffect2;
    public bool towerEffect3;
    public Sprite[] levelSprite;
    public GameObject bullet;
}
