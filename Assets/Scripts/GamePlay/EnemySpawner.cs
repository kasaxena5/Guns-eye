using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Transforms")]
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform target;
    [SerializeField] EnemyBehaviour enemyPrefab;

    [Header("Rock Configs")]
    [SerializeField] float maxRockSize;
    [SerializeField] float minRockSize;
    [SerializeField] float maxRockInitialSpeed;
    [SerializeField] float minRockInitialSpeed;
    [SerializeField] float minRockSpawnWaitTime;
    [SerializeField] float maxRockSpawnWaitTime;

    void Start()
    {
        EnemyBehaviour enemy = Instantiate(enemyPrefab, spawnTransform.position + new Vector3(Random.Range(-5, 5), 0, 0), Quaternion.identity);

        float speed = Random.Range(minRockInitialSpeed, maxRockInitialSpeed);
        Vector2 direction = Random.insideUnitCircle;
        Debug.Log(direction);
        enemy.Initialize(direction, speed, target);
        //StartCoroutine(CubeEnemySpawner());
    }

    IEnumerator CubeEnemySpawner()
    {
        while (true)
        {
            /*
            float spawnWaitTime = Random.Range(minBatSpawnWaitTime, maxBatSpawnWaitTime);

            BatEnemy bat = Instantiate(batPrefab, batSpawner.position + new Vector3(Random.Range(-5, 5), 0, 0), Quaternion.identity);

            float speed = Random.Range(minBatInitialSpeed, maxBatInitialSpeed);
            Vector2 direction = Random.insideUnitCircle;
            bat.Initialize(direction, speed, batTarget);

            yield return new WaitForSeconds(spawnWaitTime);
            */
        }
    }
}
