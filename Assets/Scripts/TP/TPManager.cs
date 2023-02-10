using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{   
    private int regenRate;
    private WaitForSeconds regenTime; 

    public int regenTPAmount = 1;
    
    void Start()
    {
        regenTime = new WaitForSeconds(PlayerStats.regenRateTP);
        StartCoroutine(RegenerateTP());
    }

    void Update()
    {

    }

    // This is what makes the TP actually regenerate
    IEnumerator RegenerateTP()
    {
        while (PlayerStats.TP < PlayerStats.maxTP)
        {
            PlayerStats.TP += regenTPAmount;
            yield return regenTime;
        }
    }

    public static void WaveCompleteTPBonus()
    {
        PlayerStats.TP += PlayerStats.waveTPReward;
        Debug.Log("BONUS GIVEN");
    }
}
