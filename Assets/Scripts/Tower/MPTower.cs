using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPTower : Tower
{
    [Header("MP Tower Specifics")]
    
    [SerializeField] private float towerHP;
    [SerializeField] private float regenMPAmount;
    [SerializeField] private float slowEffect;
    [SerializeField] private float dmgAmplify;
    [SerializeField] private int vanishAfterWaves;
    [SerializeField] private Image healthBar;

    public float TowerHP{ get{return towerHP;} }

    private float startingTowerHP;
    private int wavesPassed;

    private int enemyDamage = 1;
    private int bossDamage = 10;

    
    void Awake()
    {
        WaveSpawner.OnNewWave += RegenMP;
        startingTowerHP = towerHP;
    }

    void OnDisable() 
    {
        WaveSpawner.OnNewWave -= RegenMP;  // Prevents OnNewWave event from triggering a regen after object is disabled/destroyed.
    }

    void OnTriggerEnter(Collider _gameObject) 
    {
        if (_gameObject.gameObject.tag == enemyTag)
        {
            ReduceTowerHP(enemyDamage);
            ApplyEnemyDebuff(_gameObject.GetComponent<Enemy>());
            DisplayTowerHP();
            return;
        }

        if (_gameObject.gameObject.tag == bossTag)
        {
            ReduceTowerHP(bossDamage);
            DisplayTowerHP();
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
        wavesPassed += 1;

        if (wavesPassed == vanishAfterWaves)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyEnemyDebuff(Enemy _enemy)
    {
        if (TowerAttackType == AttackType.Slower)
            _enemy.Slow(slowEffect);

        else if (TowerAttackType == AttackType.DamageAmper)
            _enemy.DamageAmplify(dmgAmplify);
    }

    void DisplayTowerHP()
    {
        healthBar.fillAmount = towerHP / startingTowerHP;
    }
}
