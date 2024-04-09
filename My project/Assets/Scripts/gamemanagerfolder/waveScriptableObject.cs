using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/wave")]

public class waveScriptableObject : ScriptableObject
{

    public string[] enemiesInWave;
    public float timeAfterWave;
    public string wavePath;

}
