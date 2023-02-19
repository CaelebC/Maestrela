using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Boss Properties")]
    public GameObject enemyToSpawn;
    public int numMinions;
    private float spawnInterval = 0.3f;
    
    public override void EnemyDefeated()
    {
        WaveSpawner.enemiesAlive--;

        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);
        Destroy(defeatEffect, 1f);
        
        StartCoroutine(SpawnMinion());
    }

    IEnumerator SpawnMinion()
    {
        for (int i = 0; i < numMinions; i++)
        {
            GameObject minionsSpawned = Instantiate(enemyToSpawn.gameObject, transform.position, Quaternion.identity);
            minionsSpawned.GetComponent<Enemy>().isMinion = true;
            // Debug.Log("spawned");
            yield return new WaitForSeconds(spawnInterval);
        }
        
        DestroyThisGameObject();
        yield break;
    }

    void DestroyThisGameObject()
    {
        // Debug.Log("gameObject destroyed");
        Destroy(gameObject);
    }
}
