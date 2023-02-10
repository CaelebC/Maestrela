using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardSO : ScriptableObject
{    
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;

    public bool isSidegrade;

    public float efMaxTP;
    public int efTPRegenAmount;
    public float efTPGive;

    public float efMaxMP;
    public float efMPDrainRate;
    public float efMPGive;

    // public float efMobsMoveSpeed;
    // public float efMobsHP;

    // public float efElectronicDMG;
    // public float efElectronicROF;

    // public float efToolsDMG;
    // public float efToolsROF;

    // public float efWritingDMG;
    // public float efWritingROF;

    // public float efNecessitiesDMG;
    // public float efNecessitiesROF;

    // public bool efBurnoutNull;
    // public float efWaveCompleteReward;
    // public float efTowerUpgradeCost;

    public void ApplyCard()
    {
        // This is all temporary until a better way of doing it is done.

        // TP
        if (efMaxTP != 0f)
        {
            PlayerStats.maxTP = Mathf.RoundToInt( (PlayerStats.maxTP) * efMaxTP );
        }

        if (efTPRegenAmount != 0f)
        {
            PlayerStats.regenAmountTP = PlayerStats.regenAmountTP + efTPRegenAmount;
        }

        if (efTPGive != 0f)
        {
            PlayerStats.TP += Mathf.RoundToInt( (PlayerStats.maxTP) * efTPGive );
            // Probably need a check so that it doesn't go over the maximum
        }

        // MP
        if (efMaxMP != 0)
        {
            PlayerStats.maxMP = PlayerStats.maxMP * efMaxMP;
        }

        if (efMPDrainRate != 0)
        {
            PlayerStats.drainRateMP = PlayerStats.drainRateMP * efMPDrainRate;
        }

        if (efMPGive != 0)
        {
            PlayerStats.MP += PlayerStats.maxMP * efMPGive;
            // Probably need a check so that it doesn't go over the maximum
        }

        Debug.Log("card applied");
    }

}
