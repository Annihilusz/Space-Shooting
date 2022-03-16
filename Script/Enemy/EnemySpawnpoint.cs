using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public GameObject enemy;

    float maxSpwanRateInnSeconds = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();

    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpwanRateInnSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpwanRateInnSeconds);
        }
        else
        {
            spawnInSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if(maxSpwanRateInnSeconds > 1f)
        {
            maxSpwanRateInnSeconds--;
        }

        if(maxSpwanRateInnSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }
    public void ScheduleEnemySpawn()
    {
        maxSpwanRateInnSeconds = 3f;

        Invoke("SpawnEnemy", maxSpwanRateInnSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
