using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TP;
    public int startingTP = 50;
    public int maxTP;
    
    public static int HP;
    public int startingHP;

    public static float MP;
    public float startingMP;
    public static float maxMP;
    public float startingMaxMP;
    public static float drainRateMP;
    public float startingDrainRateMP;

    public static int waves;


    void Start()
    {
        TP = startingTP;
        HP = startingHP;
        MP = startingMP;
        maxMP = startingMaxMP;
        drainRateMP = startingDrainRateMP;
        waves = 0;
    }
}
