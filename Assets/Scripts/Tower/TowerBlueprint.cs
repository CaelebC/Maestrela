using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    // TowerBlueprint is the 'data' that is sent to the 
    // BuildManager, and this is where the cost is actually 
    // obtained and used to subtract from the TP the player has.

    // POSSIBLY REMOVE THIS CLASS, OR FIX IT IN THE BUILD MANAGER.
    
    public GameObject prefab;
    public int cost;

    public bool isMPTower;
    
    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellPrice()
    {
        return cost/2;
    }
}
