using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int HP;
    public int startingHP;
    
    public static int TP;
    public int startingTP;
    public static int maxTP;
    public int startingMaxTP;
    public static int regenRateTP;
    public int startingRegenRateTP;
    public static int waveTPReward;
    public int startingWaveTPReward;

    public static float MP;
    public float startingMP;
    public static float maxMP;
    public float startingMaxMP;
    public static float drainRateMP;
    public float startingDrainRateMP;

    public static int waves;

    void Awake()
    {
        HP = startingHP;

        TP = startingTP;
        maxTP = startingMaxTP;
        regenRateTP = startingRegenRateTP;
        waveTPReward = startingWaveTPReward;

        MP = startingMP;
        maxMP = startingMaxMP;
        drainRateMP = startingDrainRateMP;

        waves = 0;
    }

    public int GetTP()
    {
        return TP;
    }

    public int GetMaxTP()
    {
        return maxTP;
    }

    public int GetRegenRateTP()
    {
        return regenRateTP;
    }
}
