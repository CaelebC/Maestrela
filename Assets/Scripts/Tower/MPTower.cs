using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPTower : Tower
{
    [Header("MP Tower Specifics")]
    
    [SerializeField] private int towerHP;
    [SerializeField] private float regenMPAmount;
    
    [SerializeField] private float slowEffect;
    public float GetSlowEffect{ get{return slowEffect;} }
    [SerializeField] private float dmgAmplify;
    public float GetDmgAmplify{ get{return dmgAmplify;} }

    [SerializeField] private int vanishAfterWaves;
    private int wavesPassed;

    private int enemyDamage = 1;
    private int bossDamage = 10;

    
    void Awake()
    {
        WaveSpawner.OnNewWave += RegenMP;
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
        wavesPassed += 1;

        if (wavesPassed == vanishAfterWaves)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyEnemyDebuff(Enemy _enemy)
    {
        Debug.Log(_enemy.speed);
        if (TowerAttackType == AttackType.Slower)
        {
            Debug.Log("SLOW WAS APPLIED");
            _enemy.Slow(slowEffect);
        }
        else if (TowerAttackType == AttackType.DamageAmper)
        {
            _enemy.DamageAmplify(dmgAmplify);
        }
    }
}
