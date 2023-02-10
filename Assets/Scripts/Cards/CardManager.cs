using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // public CardSO selectedCard;

    public EventHandler OnWaveInterval;
    public GameObject cardSelectionUI;

    public int cardInterval;

    
    void Start()
    {
        WaveSpawner waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.OnNewWave += Test_OnNewWave;
    }

    void Update()
    {
        // selectedCard.ApplyCard();
        // Debug.Log("Card applied");
    }

    private void Test_OnNewWave(object sender, WaveSpawner.OnNewWaveArgs e)
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

    
}
