using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // Modify movement speed in the Enemy.cs script    
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemyStats;


    void Start()
    {
        enemyStats = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemyStats.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        enemyStats.speed = enemyStats.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= (Waypoints.points.Length - 1))
        {
            EndPath();
            return;
        }
        
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.HP -= 10;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
