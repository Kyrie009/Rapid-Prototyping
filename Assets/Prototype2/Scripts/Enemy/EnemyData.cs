using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    //Scriptable object that stores enemydata
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int health;
        public int attack;
        public int exp;
    }
}
