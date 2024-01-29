using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator EnemySpawning()
    {
        float delay = 5.0f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            Instantiate(enemy);
        }
    }
}
