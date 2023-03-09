using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardSO : ScriptableObject
{    
    [Header("Card Identity")]
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    public bool isSidegrade;

    [Header("TP Stats")]
    public float efMaxTP;
    public int efTPRegenAmount;
    public float efTPGive;

    [Header("MP Stats")]
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

}
