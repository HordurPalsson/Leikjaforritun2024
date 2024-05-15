using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player; // Staðsetning leikmannsins
    public float spawnRadius = 10f; // Radius þar sem óvinir geta birst
    public float spawnInterval = 5f; 

    private float lastSpawnTime;
    private void Start()
    {
        lastSpawnTime = Time.time;
        ClearEnemies();
    }

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time > lastSpawnTime + spawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time; // Reset last spawn time
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius; // Get a random position in a circle around the player
        spawnPos += (Vector2)player.position; // Adjust position relative to the player's location


        // Randomly pick an enemy prefab
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[index];


        // Instantiate the enemy at the calculated position
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

    void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Destroy each enemy found
        }
    }
}
