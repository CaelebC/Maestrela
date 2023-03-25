using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPTower : Tower
{
    [Header("MP Tower Stats")]
    [SerializeField] float towerHP;
    [SerializeField] float regenMPPercent;
    [SerializeField] int vanishAfterWaves;
    [SerializeField] float slowEffectPercent;
    [SerializeField] float dmgAmpEffectPercent;
    
    [SerializeField] Image healthBar;

    public float TowerHP{ get{return towerHP;} }
    public float RegenMPPercent{ get{return regenMPPercent;} }
    public float VanishAfterWaves{ get{return vanishAfterWaves;} }

    public (float, string) TowerEffect { 
        get {
            if (slowEffectPercent != 0) return (slowEffectPercent, "Slow");
            else if (dmgAmpEffectPercent!= 0) return (dmgAmpEffectPercent, "DMG Amp");
            else return (0f, "null");  // null float could not be accepted, so 0f is returned instead
        }
    }

    private float startingTowerHP;
    private int wavesPassed;

    private int enemyDamage = 1;
    private int bossDamage = 9999;

    
    void Awake()
    {
        WaveSpawner.OnNewWave += RegenMP;
        startingTowerHP = towerHP;
        typeHexColor = EntityTypeColor.TypeColor(TowerEntityType);
        ColorUtility.TryParseHtmlString(typeHexColor, out towerBaseColor);
        
        towerBaseRenderer.material.color = towerBaseColor;
        towerGameObjImage.sprite = towerSprite;
    }

    void OnDestroy() 
    {
        // Prevents OnNewWave event from triggering a regen after object is disabled/destroyed. 
        // Also just good practice to get rid of listeners when destroyed/disabled
        WaveSpawner.OnNewWave -= RegenMP; 

        // Because OnDestroy() is re-stated in this inherited script, some of the
        // stuff needs to be restated here as well
        PlayerStats.numBuiltTowers -= 1; 
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
        PlayerStats.MP += (PlayerStats.maxMP * RegenMPPercent);
        wavesPassed += 1;

        if (wavesPassed == vanishAfterWaves)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyEnemyDebuff(Enemy _enemy)
    {
        if (TowerAttackType == AttackType.Slower)
            _enemy.Slow(slowEffectPercent);

        else if (TowerAttackType == AttackType.DamageAmper)
            _enemy.DamageAmplify(dmgAmpEffectPercent);
    }

    void DisplayTowerHP()
    {
        healthBar.fillAmount = towerHP / startingTowerHP;
    }
}
