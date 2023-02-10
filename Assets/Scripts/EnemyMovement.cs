using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int rewardTP = 2;
    private Transform target;
    private int wavepointIndex = 0;
    public GameObject defeatParticle;
    

    void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            EnemyDefeated();
        }
    }

    void EnemyDefeated()
    {
        PlayerStats.TP += rewardTP;
        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);

        Destroy(defeatEffect, 5f);
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= (Waypoints.points.Length - 1))
        {
            EndPath();
            return;
        }
        
        wavepointIndex += 1;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.HP -= 10;
        Destroy(gameObject);
    }
}
