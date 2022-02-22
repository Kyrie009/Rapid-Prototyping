using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class SpawnManager : MonoBehaviour
    {
        private float spawnRange = 9;
        public int enemyCount;
        public int waveNumber = 1; //number of enemies spawn according to the wave number

        public GameObject enemyPrefab;
        public GameObject powerupPrefab;
        
        void Start()
        {
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }

        private void Update()
        {
            //find all enemies in level
            enemyCount = FindObjectsOfType<Enemy>().Length;
            //if no enemies in level start next wave
            if (enemyCount == 0)
            {
                waveNumber++;//increases spawn of the next wave
                SpawnEnemyWave(waveNumber);
            }
        }
        private Vector3 GenerateSpawnPosition()
        {
            //area where the enemy can spawn 
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);
            Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
            return randomPos;
        }     
        public void SpawnEnemyWave(int _enemiesToSpawn)
        {
            //spawns a new powerup
            SpawnPowerup();
            //spawn enemies
            for (int i = 0; i<_enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }
        public void SpawnPowerup()
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }


    }
}

