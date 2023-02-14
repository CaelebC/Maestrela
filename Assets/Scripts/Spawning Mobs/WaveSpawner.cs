using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public GameManager gameManager;
	
	// Event handler things. For this script to send out a 'signal' to listeners
	public static event Action<int> OnNewWave;
	public static event Action<int> OnTotalWavesObtain;

    // The states of the spawning mechanism. 
    // !!! SHOULD NOT BE CHANGED IN THE UNITY EDITOR !!!
    private enum SpawnState { SPAWNING, WAITING, COUNTING };
    
    public Transform[] spawnPoints;
    public Wave[] waves;

	public static int enemiesAlive = 0;
	private int nextWave = 0;
	public int currentWave
	{
		get { return nextWave; }
	}

    public float timeBetweenWaves = 5.0f;
	private float waveCountdown;

	// searchCountdown is the countdown between the scanning if there are enemies remaining in the wave.
    private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;

	// For randomization of mobs
	private double accumulatedWeights;
	private System.Random rand = new System.Random();	


	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced");
		}

		waveCountdown = timeBetweenWaves;
		// The ?.Invoke() is a null checker. If it isn't null, then the Invoke() will be 'ran'.
		OnTotalWavesObtain?.Invoke(waves.Length);
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );
				OnNewWave?.Invoke(currentWave);
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		// Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}
		else
		{
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;
		int totalEnemyCount = _wave.totalEnemyCount;

		EnemyWeights[] enemyWeights = _wave.enemiesToSpawn;
		foreach (EnemyWeights enemy in enemyWeights)
		{
			int numToSpawn = (int)Math.Floor((enemy.spawnChance / 100) * totalEnemyCount);
			
			for (int i = 0; i < numToSpawn; i++)
			{
				SpawnEnemy(enemy.enemyPrefab);
				yield return new WaitForSeconds(1f / _wave.spawnRate);
			}
		}

		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		// Debug.Log("Spawning Enemy: " + _enemy.name);		
		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}

	// // This is where the randomization/weight calculations happen
	// // For now it is commented out.

	// int GetRandomEnemyIndex() 
	// {
    // 	double r = rand.NextDouble() * accumulatedWeights ;

	// 	for (int i = 0; i < enemies.Length; i++)
	// 	{
	// 		if (enemies[i]._weight >= r)
	// 			return i;
	// 	}
	// 	return 0;
   	// }

	// void CalculateWeights() 
	// {
	// 	accumulatedWeights = 0f;
	// 	foreach (Enemy enemy in enemies) 
	// 	{
	// 		accumulatedWeights += enemy.Chance;
	// 		enemy._weight = accumulatedWeights;
	// 	}
	// }

}
