using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public int baseAtk;
    public int totalAtk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI4>() != null)
        {
            totalAtk = baseAtk;
            other.GetComponent<EnemyAI4>().Hit(totalAtk);
        }
    }
}
