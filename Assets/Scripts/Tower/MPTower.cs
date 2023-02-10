using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPTower : MonoBehaviour
{
    [Header("MP Tower Stats")]
    public int price;
    public int towerHP;
    public float regenMPAmount;
    public float slowEffect;
    public float dmgAmplify;

    [HideInInspector] public string enemyTag = "Enemy";
    [SerializeField] private int enemyDamage;

    [HideInInspector] public string bossTag;
    [SerializeField] private int bossDamage;

    
    void Awake()
    {
        WaveSpawner.OnNewWave += RegenMP;
    }

    void OnTriggerEnter(Collider _gameObject) 
    {
        if (_gameObject.gameObject.tag == enemyTag)
        {
            ReduceTowerHP(enemyDamage);
            // ApplyEnemyDebuff();
            return;
        }

        if (_gameObject.gameObject.tag == bossTag)
        {
            ReduceTowerHP(bossDamage);
            return;
        }
        
    }

    void ReduceTowerHP(int damage)
    {
        towerHP -= damage;

        if (towerHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void RegenMP(int num)
    {
        PlayerStats.MP += regenMPAmount;
    }

    void ApplyEnemyDebuff(Enemy enemy)
    {
        enemy.speed -= slowEffect;
    }
}
