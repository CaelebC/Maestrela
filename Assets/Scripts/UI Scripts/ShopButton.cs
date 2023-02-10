using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject towerBlueprint;

    public int GetCost(TowerBlueprint towerBlueprint)
    {
        return towerBlueprint.cost;
    }
}
