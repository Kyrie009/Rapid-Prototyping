using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class MeleeHitbox : GameBehaviour
    {
        public WeaponType weapon;
        public int baseAtk;
        public int totalAtk;

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<EnemyAI>() != null)
            {
                totalAtk = baseAtk + _UI2.charStat.atk;
                other.GetComponent<EnemyAI>().Hit(totalAtk);
            }
        }

    }
}

