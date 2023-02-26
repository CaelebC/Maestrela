using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    public static int HP;
    public int startHP;
    
    public static int TP;
    public int startTP;
    public static int maxTP;
    public int startMaxTP;
    public static int regenAmountTP;
    public int startRegenAmountTP;
    public static int waveTPReward;
    public int startWaveTPReward;

    public static float MP;
    public float startMP;
    public static float maxMP;
    public float startMaxMP;
    public static float drainRateMP;
    public float startDrainRateMP;

    public static int maxTowerSpace;  // Tower space is essentially the number of towers built. Having this limits the number of built towers.
    public int startMaxTowerSpace;
    [HideInInspector] public static int numBuiltTowers;

    public static int waves;

    [SerializeField] private static float damageReduction = 0.5f;
    public static float DamageReduction { get{return damageReduction;} }


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

        instance = this;
    }

    void WaveCount(int _waveNumber)
    {
        waves = _waveNumber;
    }
}
