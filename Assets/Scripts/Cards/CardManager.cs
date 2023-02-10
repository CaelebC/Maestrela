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
        Debug.Log("event handler works" + e.waveNumberArgs);
        // if ( (PlayerStats.waves / cardInterval) == 0)
        // {
        //     // cardSelectionUI.Toggle();
        //     OnWaveInterval?.Invoke(this, EventArgs.Empty);  // Only allows for event to 'happen' if not null.
        // }
    }

    
}
