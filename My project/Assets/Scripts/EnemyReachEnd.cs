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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            int x = int.Parse(life.text);
            x -= 1;
            life.text = x.ToString();
            Destroy(collision.gameObject);
        }
    }
}
