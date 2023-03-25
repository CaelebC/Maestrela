using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{   
    CardManager cardManager;
    [HideInInspector] public CardSO cardData;

    [Header("Unity Inspector Assignments")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public Image cardSprite;

    void Start()
    {
        cardManager = CardManager.instance;
    }

    private void OnEnable() 
    {
        cardName.text = cardData.cardName;
        cardDescription.text = cardData.cardDescription;
        if (cardData.cardSprite)
            cardSprite.sprite = cardData.cardSprite;
    }

    public void PlayerSelected()
    {
        // Debug.Log("PlayerSelected() ran");
        cardManager.PlayerSelectedCard(cardData);
    }

}
