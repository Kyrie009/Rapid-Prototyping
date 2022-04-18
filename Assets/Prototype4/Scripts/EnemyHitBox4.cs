using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox4 : MonoBehaviour
{
    EnemyAI4 enemy;
    void Start()
    {
        enemy = FindObjectOfType<EnemyAI4>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player4>() != null)
        {
            other.GetComponent<Player4>().Hit(enemy.attack);
        }
    }
}
