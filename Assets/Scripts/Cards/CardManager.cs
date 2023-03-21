using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<CardSO> cardsFromShuffle;
    [HideInInspector] public List<CardSO> playerSelectedCards = new List<CardSO>();

    [Header("Unity Setup Fields")]
    public CardUI[] cardButtonPool;
    public GameObject cardSelectionUI;

    // Sum of effects on stats
    private float sumEfMaxTP = 1.0f;
    private int sumEfTPRegenAmount = 0;
    private int sumEfWaveTPReward = 0;

    private float sumEfMaxMP = 1.0f;
    private float sumEfMPDrainRate = 0f;

    private float sumEfDamageMultiplier = 1.0f;
    // private float sumEfFireRate = 1.0f;
    // private int sumEfUpgradeCostReductionValue = 0;
    private int sumEfTowerSpace = 0;

    private float sumEfEnemyMoveSpeed = 1.0f;
    private float sumEfEnemyHP = 1.0f;

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
            List<CardSO> cards = CardShuffler();
            // Debug.Log(cards);

            if (cards == null || cards.Count == 0)
            {
                Debug.LogError("NO MORE CARDS. CARD SELECTION SKIPPED");
                return;
            }
            
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

    private List<CardSO> CardShuffler()
    {
        cardsFromShuffle = new List<CardSO>();

        if (cardsList.Count == 0)
        {
            return null;
        }
        if (cardsList.Count <= numCardChoices)
        {
            return cardsList;
        }
        
        for(int i = 0; i < numCardChoices; i++)
        {
            bool successful = false;
            while (!successful)
            {
                int randomNumber = Random.Range(0, cardsList.Count);
                CardSO randomCard = cardsList[randomNumber];

                if ( cardsFromShuffle.Contains(randomCard) )
                {
                    continue;
                }
                else
                {
                    cardsFromShuffle.Add(randomCard);
                    successful = true;
                }
            }

            // Debug.Log("RANDOM card selected");
        }
        if (cardsFromShuffle.Count == numCardChoices)
        {
            // Debug.Log("cards have been selected");
            return cardsFromShuffle;
        }

        Debug.LogError("No random card selected");
        return null;
    }

    public void PlayerSelectedCard(CardSO _selectedCard)
    {
        playerSelectedCards.Add(_selectedCard);

        // Removing player selected cards 
        // There's a better and more efficient way of doing this using 'list.FindIndex' or something else. But as of right now I don't know how.        
        for (int i = 0; i < cardsList.Count; i++)
        {
            if (_selectedCard == cardsList[i])
                cardsList.RemoveAt(i);
        }

        // Applying card effect
        ApplyCard(_selectedCard);
        ShowCardsSprites();

        // Removes data from the button game objects, and sets them inactive (to be activated again when needed)
        foreach (CardUI _cardButton in cardButtonPool)
        {
            _cardButton.cardData = null;
            _cardButton.gameObject.SetActive(false);
        }

        // Unpauses the game after card selection
        if (cardSelectionUI.activeSelf)
        {
            Time.timeScale = 1f;
            cardSelectionUI.SetActive(false);
        }

    }

    public void ApplyCard(CardSO _selectedCard)
    {
        ApplyTPEffects(_selectedCard);
        ApplyMPEffects(_selectedCard);
        ApplyTowerEffects(_selectedCard);
        ApplyEnemyEffects(_selectedCard);
    }

    void ApplyTPEffects(CardSO _selectedCard)
    {
        // --Max TP--
        sumEfMaxTP += _selectedCard.maxTPPercent;
        PlayerStats.maxTP = Mathf.RoundToInt(pStats.startMaxTP * sumEfMaxTP);
        
        // --TP Regen Amount--
        sumEfTPRegenAmount += _selectedCard.TPRegenAmountValue;
        PlayerStats.regenAmountTP = pStats.startRegenAmountTP + sumEfTPRegenAmount;
        
        // --TP Give/Take--
        int computedTP = Mathf.RoundToInt(PlayerStats.maxTP * _selectedCard.TPGivePercent);
        if ( PlayerStats.TP + computedTP >= PlayerStats.maxTP )  // true when given TP will be greater than or equal to the max
        {
            PlayerStats.TP = PlayerStats.maxTP;
        }
        else if ( PlayerStats.TP + computedTP <= 0 )  // true when taken TP will be less than or equal to 0
        {
            PlayerStats.TP = 0;
        }
        else  // just gives/takes the TP
        {
            PlayerStats.TP += computedTP;
        }
        
        // --Wave TP Reward--
        sumEfWaveTPReward += _selectedCard.waveTPRewardValue;
        PlayerStats.waveTPReward += _selectedCard.waveTPRewardValue;
    }

    void ApplyMPEffects(CardSO _selectedCard)
    {
        // --Max MP--
        sumEfMaxMP += _selectedCard.maxMPPercent;
        PlayerStats.maxMP = Mathf.RoundToInt(pStats.startMaxMP * sumEfMaxMP);
        
        // --MP Drain Rate--
        sumEfMPDrainRate += _selectedCard.MPDrainRateValue;
        PlayerStats.drainRateMP = pStats.startDrainRateMP - _selectedCard.MPDrainRateValue;  // This is value (not percent) because having percentages will make cards with a drain reduction very OP
        
        // --MP Give/Take--
        float computedMP = PlayerStats.maxMP * _selectedCard.MPGivePercent;
        if ( PlayerStats.MP + computedMP >= PlayerStats.maxMP )  // true when given MP will be greater than or equal to the max
        {
            PlayerStats.MP = PlayerStats.maxMP;
        }
        else if ( PlayerStats.MP + computedMP <= 0 )  // true when taken MP will be less than or equal to 0
        {
            PlayerStats.MP = 0.1f;
        }
        else // just gives/takes the MP
        {
            PlayerStats.MP += computedMP;
        }
    }

    void ApplyTowerEffects(CardSO _selectedCard)
    {
        // --Tower Damage Multiplier--
        sumEfDamageMultiplier += _selectedCard.damageMultiplierPercent;
        PlayerStats.towerDamageMultiplier = sumEfDamageMultiplier;
        
        // --Tower Space--
        sumEfTowerSpace += _selectedCard.towerSpaceValue;
        PlayerStats.maxTowerSpace = pStats.startMaxTowerSpace + sumEfTowerSpace;
    }

    void ApplyEnemyEffects(CardSO _selectedCard)
    {
        // --Enemy Move Speed--
        sumEfEnemyMoveSpeed += _selectedCard.enemyMoveSpeedPercent;
        PlayerStats.enemyMoveSpeedMultiplier = sumEfEnemyMoveSpeed;
        
        // --Enemy HP--
        sumEfEnemyHP += _selectedCard.enemyHPPercent;
        PlayerStats.enemyHPMultiplier = sumEfEnemyHP;
    }

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
    }
}
