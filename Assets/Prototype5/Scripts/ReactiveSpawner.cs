using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveSpawner : GameBehaviour
{   
    public int initialEnemyCount;
    int totalEnemyCount;
    public int enemies = 5; //number of enemies
    public float spawnTime = 5f;
    public float detectionRange;
    public float spawnRange = 2;
    public float spawnArea = 2;
  
    public GameObject enemyPrefab;
    public LayerMask whatIsPlayer;

    Vector3 spawn;
    bool canSpawn = false;
    bool isInitialWave = true;
    bool isPlayerInRange;
    bool isPlayerInSpawnArea;


    void Start()
    {
        spawn = GetComponent<Transform>().position;
        canSpawn = true;
    }

    private void Update()
    {
        EnemyAI4[] enemyCount = FindObjectsOfType<EnemyAI4>();
        isPlayerInRange = Physics.CheckSphere(transform.position, detectionRange, whatIsPlayer); // check if play in range of spawner
        isPlayerInSpawnArea = Physics.CheckSphere(transform.position, spawnRange, whatIsPlayer); // check if play in range of enemy spawn point
        if (isPlayerInRange && !isPlayerInSpawnArea && enemyCount.Length <= 20)
        {
            EnableEnemySpawn();
        }
    }
    //Enables the enemy spawn check
    private void EnableEnemySpawn()
    {
        if (canSpawn)
        {
            canSpawn = false;
            if (isInitialWave) //initial wave of enemy + increment
            {
                isInitialWave = false;
                Spawnenemies(initialEnemyCount);
                canSpawn = true;
            }
            else
            {
                StartCoroutine(SpawnOverTime()); //if player is in the area spawn more enemies as time passes
            }
        }
    }
    //spawn enemies after time passes
    IEnumerator SpawnOverTime()
    {
        SpawnNextWave();
        yield return new WaitForSeconds(spawnTime);
        canSpawn = true;
    }
    //initiate wave
    public void SpawnNextWave()
    {
        Spawnenemies(enemies);
    }
    //spawns enemies
    public void Spawnenemies(int _enemiesToSpawn)
    {        
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    //retrieves designated spawn position for enemy
    private Vector3 GenerateSpawnPosition()
    {
        //area where the enemy can spawn 
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawn.x + spawnPosX, spawn.y, spawn.z + spawnPosZ);
        return randomPos;
    }
}
