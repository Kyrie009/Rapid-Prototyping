using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class CharacterStatus : GameBehaviour
    {
        //Stats
        public string playerName;
        public int maxHealth;
        public int health;
        public int atk;
        public int playerLevel;
        public int maxExp;
        public int exp;

        void Start()
        {
            GetDefaults();
        }

        public void GetDefaults()
        {
            maxHealth = 1000;
            health = maxHealth;
            atk = 100;
            playerLevel = 1;
            exp = 0;
            maxExp = 100;
        }

        //Takes Hit
        public void Hit(int _dmg)
        {
            if (!IsDead())
            {
                health -= _dmg;
                _UI2.UpdatePlayerStatus();
                if (IsDead())
                {
                    Kill();
                }
            }
        }

        private void Kill() //Kill code
        {
            //Gameover

        }

        public bool IsDead() //Check if enemy dead
        {
            return health <= 0;
        }

        public void RewardExp(int _exp)
        {
            exp += _exp;
            if (exp >= maxExp)
            {
                playerLevel++;
                exp = 0;
            }
            _UI2.UpdatePlayerStatus();
        }

    }

}

