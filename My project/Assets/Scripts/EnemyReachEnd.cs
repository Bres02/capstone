using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyReachEnd : MonoBehaviour
{
    [SerializeField] public TMP_Text life;
    public GameObject gameManager;

    private void Start()
    {

        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.GetComponent<GameManager>().enemyReachEnd(collision.gameObject);
    }
}
