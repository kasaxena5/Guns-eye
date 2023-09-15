using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Transforms")]
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform target;
    [SerializeField] EnemyBehaviour enemyPrefab;

    [Header("Enemy Configs")]
    [SerializeField] float initialSpeed;
    [SerializeField] float spawnWaitTime;
    [SerializeField] float progressionDelta;
    [SerializeField] float progressionTime;

    void Start()
    {
        StartCoroutine(Progression());
        StartCoroutine(CubeEnemySpawner());
    }

    IEnumerator CubeEnemySpawner()
    {
        while (true)
        {
            EnemyBehaviour enemy = Instantiate(enemyPrefab, spawnTransform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);

            float speed = initialSpeed;
            Vector2 randDirection = Random.insideUnitCircle;
            Vector3 direction = new Vector3(randDirection.x, 0, randDirection.y);
            enemy.Initialize(direction, speed, target);
            yield return new WaitForSeconds(spawnWaitTime);

        }
    }

    IEnumerator Progression()
    {
        while(true)
        {
            initialSpeed += progressionDelta;
            spawnWaitTime -= progressionDelta;
            yield return new WaitForSeconds(progressionTime);
        }
    }
}
