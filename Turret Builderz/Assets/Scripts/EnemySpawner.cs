using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    private int enemiesToSpawn; // This is set t 
    public GameObject EnemyPrefab;
    public Transform EnemySpawnLocation;
    public float enemySpawnDelay = 1f;

    void Start()
    {
        enemiesToSpawn = FindObjectOfType<GameManager>().enemiesLeft;

        // Begin spawn process
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Initial delay
        yield return new WaitForSeconds(3);

        for (int spawnGap = 0; spawnGap < 10; spawnGap++)
        {
            for (int i = 0; i < enemiesToSpawn / 10; i++)
            {
                SpawnEnemy(); // Spawn enemies and randomize the enemy values (speed, health etc)

                yield return new WaitForSeconds(enemySpawnDelay);
            }

            yield return new WaitForSeconds(Random.Range(4f, 6f));

        }
    }

    public void SpawnEnemy()
    {
        // Init enemy and position of enemy
        GameObject enemy = Instantiate(EnemyPrefab);

        enemy.transform.position = EnemySpawnLocation.transform.position;

        RandomizeEnemySpeed(enemy.GetComponent<Enemy>());
        RandomizeEnemyHP(enemy.GetComponent<Enemy>());


        enemy.GetComponent<Enemy>().Initialize();
    }

    public void RandomizeEnemySpeed(Enemy enemy)
    {
        float speed = Random.Range(2, 11);
        enemy.speed = speed;

        // Show prefab based on speed
        enemy.FastPrefab.SetActive(speed > 4);
        enemy.SlowPrefab.SetActive(speed <= 4);
    }

    /// <summary>
    /// Slow enemies = higher health.. Fast enemies = lower health..
    /// </summary>
    public void RandomizeEnemyHP(Enemy enemy)
    {
        float speed = enemy.speed;

        if (speed >= 2 && speed < 4) 
        {
            enemy.startHealth += Random.Range(70,110);
            
        } 
        else if (speed >= 4 && speed < 6)
        {
            enemy.startHealth += Random.Range(-30,0);
        }
        else if (speed >= 6 && speed < 8)
        {
            enemy.startHealth += Random.Range(-50, -30);
        }
        else if (speed >= 8 && speed <= 11)
        {
            enemy.startHealth += Random.Range(-85, -50);
        }
    }

}
