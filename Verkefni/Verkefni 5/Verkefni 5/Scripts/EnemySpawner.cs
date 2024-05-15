using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Breytur
    public GameObject[] enemyPrefabs; // Listi fyrir óvini
    public Transform player; // Staðsetning leikmannsins
    public float spawnRadius = 10f; // Radius þar sem óvinir geta birst
    public float spawnInterval = 5f; // Tími á milli spawns

    private float lastSpawnTime;
    private void Start()
    {
        lastSpawnTime = Time.time;
        ClearEnemies();
    }

    private void Update()
    {
        // Kíkir hvenær síðast óvinar var spawned
        if (Time.time > lastSpawnTime + spawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time; // Uppfærir last spawn time
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius; 
        spawnPos += (Vector2)player.position; 


        // velur enemy prefab sjálfkrafa
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[index];


        // Instantiate 
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

    void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Eyðir öllum óvinum sem það finnur
        }
    }
}
