using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemyWeights[] enemiesToSpawn;
    public int totalEnemyCount;
    public float spawnRate;
}

[System.Serializable]
public class EnemyWeights
{
    public GameObject enemyPrefab;
    [Range(0f, 100f)] public float spawnChance = 100f;
    [HideInInspector] public double _weight;
}