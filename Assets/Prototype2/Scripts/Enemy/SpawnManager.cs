using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class SpawnManager : GameBehaviour
    {
        private float spawnRange = 2;
        public int enemyCount;
        public int enemies = 5; //number of enemies
        public Transform[] spawncamp;

        public GameObject enemyPrefab;

        
        void Start()
        {
            Spawnenemies(15);
        }

        private void Update()
        {
            enemyCount = FindObjectsOfType<EnemyAI>().Length;
            if (enemyCount < 5)
            {
                SpawnNextWave();
            }
            //spawn enemies as time passes
            //Invoke(nameof(SpawnNextWave), 60f);  //put the invoke in an if statement of the game will break.
        }

        public void SpawnNextWave()
        {
            enemies += 1;//increases spawn of the next wave
            if (enemies < 30)//limit to amount of enemies spawned
            {
                Spawnenemies(enemies);
            }
            
        }

        private Vector3 GenerateSpawnPosition()
        {
            //Find random spawn point
            Vector3 spawn = spawncamp[Random.Range(0, spawncamp.Length)].position;
            //area where the enemy can spawn 
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);
            Vector3 randomPos = new Vector3(spawn.x + spawnPosX, 0, spawn.z + spawnPosZ);
            return randomPos;
        }     
        public void Spawnenemies(int _enemiesToSpawn)
        {
            //spawn enemies
            for (int i = 0; i<_enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }

    }
}

