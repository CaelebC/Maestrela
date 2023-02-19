using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // Modify movement speed in the Enemy.cs script    
    private Transform target;
    private int waypointIndex = 0;
    private Enemy enemyStats;
    List<Transform> waypoints = Waypoints.points;

    // For minions pathfinding
    private List<Transform> minionWaypoints;
    private int minionWaypointIndex;

    void Start()
    {
        enemyStats = GetComponent<Enemy>();
        
        if (enemyStats.isMinion)
            GetClosestWaypoint();
        else
            target = waypoints[0];
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
        if (waypointIndex >= (waypoints.Count - 1))
        {
            EndPath();
            return;
        }
        
        if (enemyStats.isMinion)
        {
            if (minionWaypointIndex >= (minionWaypoints.Count - 1))
            {
                EndPath();
                return;
            }
            minionWaypointIndex++;
            target = minionWaypoints[minionWaypointIndex];
            return;
        }

        waypointIndex++;
        target = waypoints[waypointIndex];
    }

    void GetClosestWaypoint()
    {
        // To get the nearest waypoint
        float shortestDistance = Mathf.Infinity;
        Transform nearestWaypoint = null;

        foreach (Transform waypoint in waypoints)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoint.transform.position);
            if (distanceToWaypoint < shortestDistance)
            {
                shortestDistance = distanceToWaypoint;
                nearestWaypoint = waypoint;
            }
        }

        if (nearestWaypoint != null)
        {
            target = nearestWaypoint;
        }
        else
        {
            target = null;
        }

        // To get the remaining waypoints needed to travel to
        bool adjustedWaypoints = false;
        minionWaypointIndex = 0;
        minionWaypoints = new List<Transform>(waypoints);
        while (!adjustedWaypoints)
        {
            int i = 0;
            Transform tempWaypoint = minionWaypoints[i];
            if (target != tempWaypoint)
            {
                minionWaypoints.RemoveAt(i);
                // Debug.Log("waypoint removed");
            }
            else
            {
                adjustedWaypoints = true;
            }
        }
    }

    void EndPath()
    {
        PlayerStats.HP -= enemyStats.enemyDamage;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
