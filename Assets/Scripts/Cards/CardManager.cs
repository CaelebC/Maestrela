using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public EventHandler OnWaveInterval;
    public GameObject cardSelectionUI;

    public int cardInterval;
    public int numCardChoices;

    public List<CardSO> cardsList = new List<CardSO>();
    
    void Start()
    {
        WaveSpawner waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.OnNewWave += ShowCardSelectUI;
    }

    private void ShowCardSelectUI(object sender, WaveSpawner.OnNewWaveArgs e)
    {
        int actualWaveNum = e.waveNumberArgs + 1;
        if ( (actualWaveNum) % cardInterval == 0 )
        {
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
            CardSO randomCard = cardsList[randomNumber];
            cardsList.RemoveAt(randomNumber);
            possibleCards.Add(randomCard);
        }
        
        if (possibleCards.Count == numCardChoices)
        {
            return possibleCards;
        }

        Debug.Log("no card selected");
        return null;
    }

    public void PlayerSelectedCard()
    { 
        if (cardSelectionUI.activeSelf)
        {
            Time.timeScale = 1f;
        }
        else 
        {
            Time.timeScale = 0f;
        }
        Debug.Log("PlayerSelectedCard() ran");
    }

    
}
