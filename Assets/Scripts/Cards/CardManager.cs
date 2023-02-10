using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private List<CardSO> possibleCards;

    [HideInInspector]
    public List<CardSO> playerSelectedCards = new List<CardSO>();
    PlayerStats pStats;
    public static event Action<List<Sprite>> ShowSpritesOnApply;

    // public event EventHandler<ShowSpritesOnApplyArgs> ShowSpritesOnApply;
    // public class ShowSpritesOnApplyArgs : EventArgs 
    // {
    //     public List<Sprite> cardSpriteArgs;
    // }
    

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

        pStats = PlayerStats.instance;
    }

    private void ShowCardSelectUI(object sender, WaveSpawner.OnNewWaveArgs e)
    {
        int actualWaveNum = e.waveNumberArgs + 1;
        if ( (actualWaveNum) % cardInterval == 0 )
        {
            List<CardSO> cards = CardRandomizer();
            // Debug.Log(cards);

            button1.GetComponent<CardUI>().cardData = cards[0];
            button2.GetComponent<CardUI>().cardData = cards[1];
            button3.GetComponent<CardUI>().cardData = cards[2];

            
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
        possibleCards = new List<CardSO>();
        
        for(int i = 0; i < numCardChoices; i++)
        {
            int randomNumber = Random.Range(0, cardsList.Count);
            CardSO randomCard = cardsList[randomNumber];
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

        // Removing player selected cards 
        // There's a better and more efficient way of doing this using 'list.FindIndex' or something else. But as of right now I don't know how.
        for (int i = 0; i < cardsList.Count; i++)
        {
            if (selectedCard == cardsList[i])
                cardsList.RemoveAt(i);
        }

        // Applying card effect
        ApplyCard();

        // Unpauses the game after card selection
        if (cardSelectionUI.activeSelf)
        {
            Time.timeScale = 1f;
            cardSelectionUI.SetActive(false);
        }

    }

    public void ApplyCard()
    {
        float totalEfMaxTP = 1.0f;
        int totalEfTPRegenAmount = 0;
        float totalEfTPGive = 0;

        float totalEfMaxMP = 1.0f;
        float totalEfMPDrainRate = 0;
        float totalEfMPGive = 0;
        
        for (int i = 0; i < playerSelectedCards.Count; i++)
        {
            CardSO temp = playerSelectedCards[i];

            totalEfMaxTP += temp.efMaxMP;
            totalEfTPRegenAmount += temp.efTPRegenAmount;
            totalEfTPGive += temp.efTPGive;

            totalEfMaxMP += temp.efMaxMP;
            totalEfMPDrainRate += temp.efMPDrainRate;
            totalEfMPGive += temp.efMPGive;
        }
        

        // TP Effects
        if (totalEfMaxTP != 0)
            PlayerStats.maxTP = Mathf.RoundToInt(pStats.startingMaxTP * totalEfMaxTP);
        if (totalEfTPRegenAmount != 0)
            PlayerStats.regenAmountTP = pStats.startingRegenAmountTP + totalEfTPRegenAmount;
        
        int calculatedTP = Mathf.RoundToInt(PlayerStats.maxTP * totalEfTPGive);
        if ( (calculatedTP + PlayerStats.TP) >= PlayerStats.maxTP )
            PlayerStats.TP = PlayerStats.maxTP;
        else 
            PlayerStats.TP += calculatedTP;


        // MP Effects
        if (totalEfMaxMP != 0)
            PlayerStats.maxMP = pStats.startingMaxMP * totalEfMaxMP;
        if (totalEfMPDrainRate != 0)
            PlayerStats.drainRateMP = pStats.startingDrainRateMP * totalEfMPDrainRate;

        float calculatedMP = PlayerStats.maxMP * totalEfMPGive;        
        if ( (calculatedMP + PlayerStats.MP) >= PlayerStats.maxMP )
            PlayerStats.MP = PlayerStats.maxMP;
        else 
            PlayerStats.MP += calculatedMP;

        ShowCardsSprites();
    }

    // this might need to be a static func
    public void ShowCardsSprites()
    {
        List<Sprite> cardImages = new List<Sprite>();
        
        for (int i = 0; i < playerSelectedCards.Count; i++)
        {
            CardSO temp = playerSelectedCards[i];
            cardImages.Add(temp.cardSprite);
        }
        
        // Event system to send sprites to display selected cards
        // The ?.Invoke is a null checker. If it isn't null, then the Invoke() will be 'ran'.
        ShowSpritesOnApply?.Invoke(cardImages);
        // Debug.Log("ShowCardSprites() ran");
    }
}