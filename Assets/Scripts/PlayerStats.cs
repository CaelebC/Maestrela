using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    public static int HP;
    public int startingHP;
    
    public static int TP;
    public int startingTP;
    public static int maxTP;
    public int startingMaxTP;
    public static int regenAmountTP;
    public int startingRegenAmountTP;
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
        regenAmountTP = startingRegenAmountTP;
        waveTPReward = startingWaveTPReward;

        MP = startingMP;
        maxMP = startingMaxMP;
        drainRateMP = startingDrainRateMP;

        waves = 0;

        instance = this;
    }

    // // Obtaining starting values
    // // These all use lambda operators to just simply return the value of the variables
    // // since non-static variables can't be accessed normally/freely from other scripts.
    // public int GetStartingMaxTP() => startingMaxTP;
    // public int GetStartingRegenAmountTP() => startingRegenAmountTP;
    // public int GetStartingWaveTPReward() => startingWaveTPReward;

    // public float GetStartingMaxMP() => startingMaxMP;
    // public float GetStartingDrainRateMP() => startingDrainRateMP;

}
