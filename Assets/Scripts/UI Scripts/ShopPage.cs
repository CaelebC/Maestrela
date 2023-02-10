using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPage : MonoBehaviour
{
    BuildManager buildManager;

    public TowerBlueprint tower1;
    public TowerBlueprint tower2;

    // public GameObject towerShopButton;


    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void SelectTower1()
    {
        // Debug.Log("Purchased Tower1");
        buildManager.SelectTowerToBuild(tower1);
    }

    public void SelectTower2()
    {
        // Debug.Log("Purchased Tower2");
        buildManager.SelectTowerToBuild(tower2);
    }
}
