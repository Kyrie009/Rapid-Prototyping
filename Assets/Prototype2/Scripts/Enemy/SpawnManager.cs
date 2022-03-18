using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class SpawnManager : GameBehaviour
    {
        private float spawnRange = 110;
        public int enemyCount;
        public int waveNumber = 10; //number of enemies spawn according to the wave number

        public GameObject enemyPrefab;

        
        void Start()
        {
            SpawnEnemyWave(waveNumber);

        }

        private void Update()
        {
            //find all enemies in level
            enemyCount = FindObjectsOfType<EnemyAI>().Length;
            //if no enemies in level start next wave
            if (enemyCount <= 5)
            {
                StartCoroutine(SpawnNextWave());
            }
        }

        IEnumerator SpawnNextWave()
        {
            yield return new WaitForSeconds(10);
            waveNumber += 10;//increases spawn of the next wave
            SpawnEnemyWave(waveNumber);
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
            //spawn enemies
            for (int i = 0; i<_enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }

    }
}

