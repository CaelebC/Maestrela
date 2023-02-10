using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    CardManager cardManager;
    
    [HideInInspector]
    public CardSO cardData;

    // public event EventHandler<OnCardPressArgs> OnCardPress;
    // public class OnCardPressArgs : EventArgs
    // {
    //     public CardSO cardDataArgs;
    // }

    [Header("Unity Inspector Assignments")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public Image cardSprite;

    void Start()
    {
        cardManager = CardManager.instance;

        cardName.text = cardData.cardName;
        cardDescription.text = cardData.cardDescription;
        cardSprite.sprite = cardData.cardSprite;
        // Debug.Log(cardName);
    }

    public void PlayerSelected()
    {
        Debug.Log("PlayerSelected() ran");
        // OnCardPress?.Invoke(this, new OnCardPressArgs {cardDataArgs = cardData});
        cardManager.PlayerSelectedCard(cardData);
    }

}
