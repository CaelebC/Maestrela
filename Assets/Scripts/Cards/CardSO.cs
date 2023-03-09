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

    [Header("TP Stat Modifiers")]
    public float maxTPPercent;
    public int TPRegenAmountValue;
    public float TPGivePercent;
    public int waveTPRewardValue;

    [Header("MP Stat Modifiers")]
    public float maxMPPercent;
    public float MPDrainRateValue;
    public float MPGivePercent;

    [Header("Tower Stat Modifiers")]
    public float damageMultiplierPercent;
    public float fireRatePercent;
    public int upgradeCostReductionValue;
    public int towerSpaceValue;

    [Header("Enemy Stat Modifiers")]
    public float enemyMoveSpeedPercent;
    public float enemyHPPercent;

    // public bool efBurnoutNull;

}
