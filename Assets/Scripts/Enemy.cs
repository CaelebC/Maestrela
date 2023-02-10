using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // TO BE FIXED
    
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
        WaveSpawner.enemiesAlive--;

        Destroy(defeatEffect, 3f);
        Destroy(gameObject);
    }
}
