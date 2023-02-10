using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPage : MonoBehaviour
{
    BuildManager buildManager;


    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void PurchaseTower1()
    {
        Debug.Log("Purchased Tower1");
        buildManager.SetTowerToBuild(buildManager.tower1Prefab);
    }

    public void PurchaseTower2()
    {
        Debug.Log("Purchased Tower2");
        buildManager.SetTowerToBuild(buildManager.tower2Prefab);
    }
}
