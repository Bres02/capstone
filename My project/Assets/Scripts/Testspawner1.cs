using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testspawner1 : MonoBehaviour
{
    public void createTower(GameObject tower)
    {
        Instantiate(tower, this.transform);
    }
}
