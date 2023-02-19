using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int enemyDamage = 10;
    public GameObject defeatParticle;

    public bool isMinion;
    [HideInInspector] public float speed;
    [HideInInspector] public float health;
    private bool alreadyDefeated = false;
    
    [Header("Unity Setup Fields")]
    public Image healthBar;
    

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !alreadyDefeated)
        {
            EnemyDefeated();
            alreadyDefeated = true;
        }
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
    }

    public virtual void EnemyDefeated()
    {
        WaveSpawner.enemiesAlive--;

        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);
        
        Destroy(defeatEffect, 3f);
        Destroy(gameObject);
    }
}
