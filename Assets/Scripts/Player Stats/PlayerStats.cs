using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    [Header("Health Stats")]
    public int startHP;
    public static int HP;
    
    [Header("TP Stats")]
    public int startTP;
    public static int TP;
    public static int maxTP;
    public int startMaxTP;
    public static int regenAmountTP;
    public int startRegenAmountTP;
    public static int waveTPReward;
    public int startWaveTPReward;

    [Header("MP Stats")]
    public float startMP;
    public static float MP;
    public static float maxMP;
    public float startMaxMP;
    public static float drainRateMP;
    public float startDrainRateMP;

    [Header("Level Limitations")]
    public int startMaxTowerSpace;
    public static int maxTowerSpace;  // Tower space is essentially the number of towers built. Having this limits the number of built towers.
    [HideInInspector] public static int numBuiltTowers;

    public static int waves;

    [SerializeField] private static float burnoutDamageReduction = 0.5f;
    public static float BurnoutDamageReduction { get{return burnoutDamageReduction;} }

    public float startTowerDamageMultiplier = 1.0f;
    public float startEnemyMoveSpeedMultiplier = 1.0f;
    public float startEnemyHPMultiplier = 1.0f;
    public static float towerDamageMultiplier;
    public static float enemyMoveSpeedMultiplier;
    public static float enemyHPMultiplier;


    void Awake()
    {
        HP = startHP;

        TP = startTP;
        maxTP = startMaxTP;
        regenAmountTP = startRegenAmountTP;
        waveTPReward = startWaveTPReward;

        MP = startMP;
        maxMP = startMaxMP;
        drainRateMP = startDrainRateMP;

        WaveSpawner.OnNewWave += WaveCount;

        maxTowerSpace = startMaxTowerSpace;

        towerDamageMultiplier = startTowerDamageMultiplier;
        enemyMoveSpeedMultiplier = startEnemyMoveSpeedMultiplier;
        enemyHPMultiplier = startEnemyHPMultiplier;

        instance = this;
    }

    void WaveCount(int _waveNumber)
    {
        waves = _waveNumber;
    }

    void OnDestroy()
    {
        WaveSpawner.OnNewWave -= WaveCount;
    }
}
