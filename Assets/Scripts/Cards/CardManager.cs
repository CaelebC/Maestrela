using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    public int cardInterval;
    public int numCardChoices;

    [Header("Card Buttons")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    private CardUI cardUI;
    public GameObject cardSelectionUI;

    [Header("Pool of Cards")]
    public List<CardSO> cardsList = new List<CardSO>();

    [HideInInspector]
    public List<CardSO> playerSelectedCards = new List<CardSO>();
    
    void Awake()
    {
        // Event handler for waves
        WaveSpawner waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.OnNewWave += ShowCardSelectUI;

        if (instance != null)
        {
            Debug.LogError("More than 1 card manager in scene");
            return;
        }

        instance = this;
    }

    private void ShowCardSelectUI(object sender, WaveSpawner.OnNewWaveArgs e)
    {
        int actualWaveNum = e.waveNumberArgs + 1;
        if ( (actualWaveNum) % cardInterval == 0 )
        {
            List<CardSO> cards = CardRandomizer();
            // Debug.Log(cards[0]);

            button1.GetComponent<CardUI>().cardData = cards[0];

            
            cardSelectionUI.SetActive(true);

            if (cardSelectionUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else 
            {
                Time.timeScale = 1f;
            }
        }

    }

    List<CardSO> CardRandomizer()
    {
        List<CardSO> possibleCards = new List<CardSO>();
        for(int i = 0; i < numCardChoices; i++)
        {
            int randomNumber = Random.Range(0, cardsList.Count);
            Debug.Log("randomNumber: " + randomNumber);
            Debug.Log("cardsList.Count: " + cardsList.Count);
            CardSO randomCard = cardsList[randomNumber];
            Debug.Log("randomCard: " + randomCard);
            // cardsList.RemoveAt(randomNumber);  This is problematic. Put it in the PlayerSelectedCard()
            possibleCards.Add(randomCard);
            // Debug.Log("RANDOM card selected");
        }
        
        if (possibleCards.Count == numCardChoices)
        {
            // Debug.Log("cards have been selected");
            return possibleCards;
        }

        Debug.Log("No random card selected");
        return null;
    }

    public void PlayerSelectedCard(CardSO selectedCard)
    {
        playerSelectedCards.Add(selectedCard);

        if (cardSelectionUI.activeSelf)
        {
            Time.timeScale = 1f;
            cardSelectionUI.SetActive(false);
        }
        else 
        {
            Time.timeScale = 0f;
        }

        // Debug.Log("PlayerSelectedCard() ran properly");
        // Debug.Log(selectedCard.cardName);
        // Debug.Log(playerSelectedCards.Count);
    }

    // public void ApplyCard()
    // {
    //     // This is all temporary until a better way of doing it is done.

    //     // TP Effects
    //     if (efMaxTP != 0f)
    //     {
    //         PlayerStats.maxTP = Mathf.RoundToInt( (PlayerStats.maxTP) * efMaxTP );
    //     }

    //     if (efTPRegenAmount != 0f)
    //     {
    //         PlayerStats.regenAmountTP = PlayerStats.regenAmountTP + efTPRegenAmount;
    //     }

    //     if (efTPGive != 0f)
    //     {
    //         int calculatedTP = Mathf.RoundToInt(PlayerStats.maxTP * efTPGive);
            
    //         if ( (calculatedTP + PlayerStats.TP) >= PlayerStats.maxTP )
    //         {
    //             PlayerStats.TP = PlayerStats.maxTP;
    //         }
    //         else 
    //         {
    //             PlayerStats.TP += calculatedTP;
    //         }
    //     }

    //     // MP Effects
    //     if (efMaxMP != 0)
    //     {
    //         PlayerStats.maxMP = PlayerStats.maxMP * efMaxMP;
    //     }

    //     if (efMPDrainRate != 0)
    //     {
    //         PlayerStats.drainRateMP = PlayerStats.drainRateMP * efMPDrainRate;
    //     }

    //     if (efMPGive != 0)
    //     {
    //         float calculatedMP = PlayerStats.maxMP * efMPGive;
            
    //         if ( (calculatedMP + PlayerStats.MP) >= PlayerStats.maxTP )
    //         {
    //             PlayerStats.MP = PlayerStats.maxMP;
                
    //             Debug.Log("MAX MP ALREADY");
    //             Debug.Log(PlayerStats.MP);
    //         }
    //         else 
    //         {
    //             PlayerStats.MP += calculatedMP;
    //         }
    //     }

    //     Debug.Log("card applied");
    // }


}
