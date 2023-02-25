using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : MonoBehaviour
{
    // ShopPage is the script that handles sending the 
    // tower prefab data to the BuildManager. 
    // For this to work, the tower prefab must be assigned in the
    // Unity Editor with the appropriate tower.
    
    BuildManager buildManager;


    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void BuildTowerButton(Button _button)
    {
        buildManager.buttonLastPressed = _button;
        buildManager.SelectTowerToBuild(_button.GetComponent<ShopButton>().assignedTower);

    }
}
