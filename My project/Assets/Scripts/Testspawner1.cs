using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testspawner1 : MonoBehaviour
{
    [SerializeField] GameObject tower;
    public void createTower()
    {
        Instantiate(tower, this.transform);
    }
}
