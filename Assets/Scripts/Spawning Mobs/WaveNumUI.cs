using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveNumUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    private string currentWaveNumber = "0";
    private string totalWaves = "0";

    private void Awake() 
    {
        // WaveSpawner waveSpawner = GetComponent<WaveSpawner>();
        // waveSpawner.OnNewWave += UpdateCurrentWaveNumber;
        WaveSpawner.OnNewWave += UpdateCurrentWaveNumber;
        WaveSpawner.OnTotalWavesObtain += UpdateTotalWaves;
    }

    void Update()
    {
        waveText.text = "WAVE " + currentWaveNumber + "/" + totalWaves;
        // waveText.text = "WAVE " + PlayerStats.waves.ToString();
    }

    void UpdateCurrentWaveNumber(int num)
    {
        currentWaveNumber = (num + 1).ToString();
    }

    void UpdateTotalWaves(int num)
    {
        totalWaves = (num).ToString();
    }
}

