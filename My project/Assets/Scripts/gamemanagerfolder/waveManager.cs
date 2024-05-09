using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public waveScriptableObject[] waves;
    public int wavesCount = 0;
    public int partOfWaves = 0;
    public GameObject[] startPosition;
    public float cooldownTimer;
    public float delay = 3f;
    public bool startNextWave = true;
    public bool spawningWave = false;
    public Transform end;

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
        int z = Random.Range(0, startPosition.Length);
        //spawn Enemy code
        gameObject.GetComponent<GameManager>().enemiesleft++;
        GameObject obj = gameObject.GetComponent<ObjectPooler>().SpawnFromPool(waves[wavesCount].enemiesInWave[partOfWaves], startPosition[z].transform.position, startPosition[z].transform.rotation);
        obj.GetComponent<BasicEnemy>().onSpawn(startPosition[z], end, waves[wavesCount].wavePath);
        if (partOfWaves >= waves[wavesCount].enemiesInWave.Length-1)
        {
            if (wavesCount >= waves.Length-1)
            {
                spawningWave = false;
                startNextWave = false;
                gameObject.GetComponent<GameManager>().wavesFinished = true;
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
            delay = 3f;
            partOfWaves++;
        }
    
    }
 
}
