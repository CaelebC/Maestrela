using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    [Header("Card Selection Stats")]
    public int cardInterval;
    private int numCardChoices = 3;

    [Header("Pool of Cards")]
    public List<CardSO> cardsList = new List<CardSO>();
    private List<CardSO> possibleCards;
    [HideInInspector] public List<CardSO> playerSelectedCards = new List<CardSO>();

    [Header("Unity Setup Fields")]
    public CardUI[] cardButtonPool;
    public GameObject cardSelectionUI;

    PlayerStats pStats;
    public static event Action<List<Sprite>> ShowSpritesOnApply;

    void Awake()
    {
        WaveSpawner.OnNewWave += ShowCardSelectUI;  // Event handler for waves

        if (instance != null)
        {
            Debug.LogError("More than 1 card manager in scene");
            return;
        }
        instance = this;

        pStats = PlayerStats.instance;
    }

    private void OnDestroy() {
        WaveSpawner.OnNewWave -= ShowCardSelectUI;
    }

    private void ShowCardSelectUI(int waveIndex)
    {
        int actualWaveNum = waveIndex + 1;

        // Give player card to choose at every card interval || Give player card to choose at the start of the game 
        if ( (actualWaveNum % cardInterval) == 0 || (actualWaveNum == 1))
        {
            List<CardSO> cards = CardRandomizer();
            // Debug.Log(cards);
            int i = 0;
            foreach (CardSO _card in cards)
            {
                cardButtonPool[i].gameObject.SetActive(true);
                cardButtonPool[i].cardData = _card;
                i += 1;
            }

            cardSelectionUI.SetActive(true);

            if (cardSelectionUI.activeSelf)
                Time.timeScale = 0f;
        }

    }

    List<CardSO> CardRandomizer()
    {
        possibleCards = new List<CardSO>();
        
        for(int i = 0; i < numCardChoices; i++)
        {
            bool successful = false;
            while (!successful)
            {
                int randomNumber = Random.Range(0, cardsList.Count);
                CardSO randomCard = cardsList[randomNumber];

                if (possibleCards.Count == 0)
                {
                    possibleCards.Add(randomCard);
                    successful = true;
                }
                else if (possibleCards.Count == 1)
                {
                    if (randomCard == possibleCards[0])
                        continue;
                    else
                        possibleCards.Add(randomCard);
                        successful = true;
                }
                else if (possibleCards.Count == 2)
                {
                    if ( (randomCard == possibleCards[0]) || (randomCard == possibleCards[1]) )
                        continue;
                    else
                        possibleCards.Add(randomCard);
                        successful = true;
                }
            }

            // Debug.Log("RANDOM card selected");
        }
        
        if (possibleCards.Count == numCardChoices)
        {
            // Debug.Log("cards have been selected");
            return possibleCards;
        }

        Debug.LogError("No random card selected");
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
        if (totalEfMaxTP > 1.0f)
            PlayerStats.maxTP = Mathf.RoundToInt(pStats.startMaxTP * totalEfMaxTP);
        if (totalEfTPRegenAmount != 0)
            PlayerStats.regenAmountTP = pStats.startRegenAmountTP + totalEfTPRegenAmount;
        
        int calculatedTP = Mathf.RoundToInt(PlayerStats.maxTP * totalEfTPGive);
        if ( (calculatedTP + PlayerStats.TP) >= PlayerStats.maxTP )
            PlayerStats.TP = PlayerStats.maxTP;
        else 
            PlayerStats.TP += calculatedTP;


        // MP Effects
        if (totalEfMaxMP > 1.0f)
            PlayerStats.maxMP = pStats.startMaxMP * totalEfMaxMP;
        if (totalEfMPDrainRate != 0)
            PlayerStats.drainRateMP = pStats.startDrainRateMP * totalEfMPDrainRate;

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
