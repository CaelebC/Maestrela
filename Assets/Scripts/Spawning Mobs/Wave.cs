using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    // public EnemyWeights[] enemiesToSpawn;
    public GameObject enemy;
    public int totalEnemyCount;
    public float spawnRate;
    
    
}

public class EnemyWeights
{
    public GameObject enemyPrefab;
    [Range(0f, 100f)] public float spawnChance = 100f;
    public double _weight;
}