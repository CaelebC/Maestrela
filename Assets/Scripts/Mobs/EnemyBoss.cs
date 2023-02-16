using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Boss Properties")]
    public GameObject mobToSpawn;
    public int numMinions;
    private float spawnInterval = 0.3f;
    
    public override void EnemyDefeated()
    {
        
        WaveSpawner.enemiesAlive--;

        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);
        Destroy(defeatEffect, 3f);
        
        for (int i = 0; i < numMinions; i++)
        {
            Debug.Log(i);
            StartCoroutine(SpawnMinion());
        }
        DestroyThisGameObject();
    }

    IEnumerator SpawnMinion()
    {
        GameObject minionsSpawned = Instantiate(mobToSpawn, transform.position, Quaternion.identity);
        Debug.Log("spawned");
        yield return new WaitForSeconds(spawnInterval);
    }

    void DestroyThisGameObject()
    {
        Debug.Log("gameObject destroyed");
        Destroy(gameObject);
    }
}
