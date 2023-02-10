using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{   
    private bool regenerating;
    private bool shouldRegen;

    [SerializeField]
    private float regenTime;
    
    void Start()
    {
        regenerating = false;
        shouldRegen = (!regenerating && (PlayerStats.TP < PlayerStats.maxTP));
        if (shouldRegen)
            StartCoroutine(RegenerateTP());
    }

    void Update()
    {        
        shouldRegen = (!regenerating && (PlayerStats.TP < PlayerStats.maxTP));

        if (shouldRegen)
            StartCoroutine(RegenerateTP());
        else
            return;
        
    }

    // This is what makes the TP actually regenerate
    IEnumerator RegenerateTP()
    {
        regenerating = true;
        while (PlayerStats.TP < PlayerStats.maxTP)
        {
            PlayerStats.TP += PlayerStats.regenAmountTP;
            yield return new WaitForSeconds(regenTime);
        }
        regenerating = false;
    }

    public static void WaveCompleteTPBonus()
    {
        PlayerStats.TP += PlayerStats.waveTPReward;
        // Debug.Log("BONUS GIVEN");
    }
}
