using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float startSpeed = 10f;
    private float speed;
    public float startHealth = 100f;
    private float health;
    public int rewardTP = 2;
    public GameObject defeatParticle;
    
    private Transform target;
    private int wavepointIndex = 0;

    [Header("Unity Setup Fields")]
    public Image healthBar;


    void Start()
    {
        target = Waypoints.points[0];
        health = startHealth;
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            EnemyDefeated();
        }
    }

    void EnemyDefeated()
    {
        PlayerStats.TP += rewardTP;
        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);

        Destroy(defeatEffect, 3f);
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

    // ACTUAL MOVEMENT FUNCTIONS
    // In later builds, make it so that every function above will be in a different
    // script. 
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
