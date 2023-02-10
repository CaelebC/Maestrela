using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    // Event handler things. For this script to send out a 'signal' to listeners
	public static event Action<int> OnNewWave;
	public static event Action<int> OnTotalWavesObtain;


    // The states of the spawning mechanism. 
    // !!! SHOULD NOT BE CHANGED IN THE UNITY EDITOR !!!
    private enum SpawnState { SPAWNING, WAITING, COUNTING };
    
    public Transform[] spawnPoints;

    public static int enemiesAlive = 0;
	public Wave[] waves;
	private int nextWave = 0;
	public int currentWave
	{
		get { return nextWave; }
	}

    public float timeBetweenWaves;
	private float waveCountdown;

    private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;


	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced");
		}

		waveCountdown = timeBetweenWaves;
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
				// The ?.Invoke is a null checker. If it isn't null, then the Invoke() will be 'ran'.
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
    	
	    // OnNewWave?.Invoke(currentWave);

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");
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

		for (int i = 0; i < _wave.totalEnemyCount; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.spawnRate );
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

}
