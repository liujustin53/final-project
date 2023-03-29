using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public GameObject enemyPrefab;
    public float spawnTime = 3;
    public int maxEnemies = 4;

    public float xMin = -8;
    public float xMax = 8;
    public float yMin = 1;
    public float yMax = 1;
    public float zMin = -8;
    public float zMax = 8;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update() { }

    void SpawnEnemies()
    {
        if (!LevelManager.isGameOver)
        {
            if (transform.childCount < maxEnemies)
            {
                Vector3 enemyPosition;

                enemyPosition.x = transform.position.x + Random.Range(xMin, xMax);
                enemyPosition.y = transform.position.y;
                enemyPosition.z = transform.position.z + Random.Range(zMin, zMax);

                GameObject spawnedEnemy =
                    Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;

                spawnedEnemy.transform.parent = gameObject.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(xMax - xMin, 3, zMax - zMin));
    }
}
