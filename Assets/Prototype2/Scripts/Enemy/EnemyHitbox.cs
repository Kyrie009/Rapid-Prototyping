using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class EnemyHitbox : MonoBehaviour
    {
        EnemyAI enemy;
        void Start()
        {
            enemy = gameObject.GetComponentInParent<EnemyAI>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<CharacterStatus>() != null)
            {
                other.GetComponentInParent<CharacterStatus>().Hit(enemy.attack);
            }
        }

    }
}
