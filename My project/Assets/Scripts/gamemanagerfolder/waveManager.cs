using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public waveScriptableObject[] waves;
    public int wavesCount = 0;
    public int partOfWaves = 0;
    public Transform startPosition;
    public float cooldownTimer;
    public float delay = 10f;
    public bool startNextWave = true;
    public bool spawningWave = false;

    private void Start()
    {
        cooldownTimer = 5f;
    }

    private void FixedUpdate()
    {
        if(startNextWave)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                spawningWave = true;
                startNextWave = false;
            }
        }
        else if (spawningWave)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                spawnNext();
            }
        } 
    }
    private void spawnNext()
    {
        //spawn Enemy code
        GameObject obj = gameObject.GetComponent<ObjectPooler>().SpawnFromPool(waves[wavesCount].enemiesInWave[partOfWaves], startPosition.position, startPosition.rotation);
        obj.GetComponent<BasicEnemy>().onSpawn();
        if (partOfWaves >= waves[wavesCount].enemiesInWave.Length-1)
        {
            if (wavesCount >= waves.Length-1)
            {
                spawningWave = false;
                startNextWave = false;
            }
            else
            {
                cooldownTimer = waves[wavesCount].timeAfterWave;
                spawningWave = false;
                startNextWave = true;
                partOfWaves = 0;
                wavesCount++;
            }
        }
        else
        {
            delay = 10f;
            partOfWaves++;
        }
    
    }
 
}
