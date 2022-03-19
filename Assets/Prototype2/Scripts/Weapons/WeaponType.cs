using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    [CreateAssetMenu(fileName = "WeaponType", menuName = "ScriptableObjects/WeaponType", order = 2)]
    public class WeaponType : ScriptableObject
    {
        public string weaponName;
        public int atk;
    
    }

}

