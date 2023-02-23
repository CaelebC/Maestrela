using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ITakeDamage
{
    [Header("Enemy Properties")]
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int enemyDamage = 10;
    public EntityType enemyType;

    [HideInInspector] public bool isMinion;
    [HideInInspector] public float speed;
    [HideInInspector] public float health;
    private bool alreadyDefeated = false;
    [HideInInspector] private float effectiveDmgPercent = 1.2f;
    [HideInInspector] private float notEffectiveDmgPercent = 0.8f;


    [Header("Unity Setup Fields")]
    public Image healthBar;
    public GameObject defeatParticle;
    

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

    public void TakeMoreDamage(float amount)
    {
        float computedDamage = amount * effectiveDmgPercent;
        TakeDamage(computedDamage);
        Debug.Log("DEAL MORE DAMAGE");
    }

    public void TakeLessDamage(float amount)
    {
        float computedDamage = amount * notEffectiveDmgPercent;
        TakeDamage(computedDamage);
        Debug.Log("DEAL LESSS DAMAGE");
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
