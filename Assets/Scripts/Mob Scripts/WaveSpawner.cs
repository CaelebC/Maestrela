using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public event EventHandler<OnNewWaveArgs> OnNewWave;
    public class OnNewWaveArgs : EventArgs 
    {
        public int waveNumberArgs;
    }
    
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public static int enemiesAlive = 0;
    public Wave[] waves;

    public float timeBetweenWaves = 7f;
    private float countdown = 3f;

    private int waveNumber = 0;


    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }
        if (countdown <= 0f)
        {
            if (waveNumber > 0)
            {
                TPManager.WaveCompleteTPBonus();
            }
            
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        // OnNewWave is up first so that the card selection happens before the next wave starts.
        // The ?.Invoke is a null checker. If it isn't null, then the Invoke() will be 'ran'.
        OnNewWave?.Invoke(this, new OnNewWaveArgs {waveNumberArgs = waveNumber});
        PlayerStats.waves++;
        

        Wave wave = waves[waveNumber];
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;

        if(waveNumber == waves.Length)
        {
            Debug.Log("LEVEL COMPLETED");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }
}
