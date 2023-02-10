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

    // [Header("Normal Towers")]
    public List<Tower> normTowers = new List<Tower>();
    public List<Tower> mpTowers = new List<Tower>();


    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void ButtonTowerPress()
    {

    }

    public void SelectTower0() => buildManager.SelectTowerToBuild(normTowers[0]);
    public void SelectTower1() => buildManager.SelectTowerToBuild(normTowers[1]);

    public void SelectMPTower0() => buildManager.SelectTowerToBuild(mpTowers[0]);
}
