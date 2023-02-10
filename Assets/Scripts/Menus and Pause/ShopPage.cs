using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPage : MonoBehaviour
{
    // ShopPage is the script that handles sending the 
    // tower prefab data to the BuildManager. 
    // For this to work, the tower prefab must be assigned in the
    // Unity Editor with the appropriate tower.
    
    BuildManager buildManager;

    [Header("Unity Setup Fields")]
    public TowerBlueprint tower1;
    public TowerBlueprint tower2;
    
    public TowerBlueprint mpTower1;


    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void SelectTower1() => buildManager.SelectTowerToBuild(tower1);
    public void SelectTower2() => buildManager.SelectTowerToBuild(tower2);

    public void SelectMPTower1() => buildManager.SelectTowerToBuild(mpTower1);
}
