using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // Node handles all the code that relates to the 
    // building, selling, and upgrading of towers. 
    // This is also where the detection for 
    // mouse clicks, hovers, and leaves is handled. 
    
    BuildManager buildManager;
    
    public bool forMPTowers;

    public Vector3 towerPositionOffset;

    private Color startColor;  // This is to store the 'original' color of the node, allowing the color to revert to the original after being changed a bunch.
    public Color hoverColor;
    public Color errorColor;
    
    private Renderer rend;  // Used for modifying Node GameObject's color

    [HideInInspector] public GameObject cloneTower;
    [HideInInspector] public Tower cloneTowerData;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + towerPositionOffset;
    }

    void BuildTower(Tower towerPrefab)
    {
        if(PlayerStats.TP < towerPrefab.price)
        {
            return;
        }

        PlayerStats.TP -= towerPrefab.price;
        
        cloneTower = (GameObject)Instantiate(towerPrefab.gameObject, GetBuildPosition(), Quaternion.identity);
        cloneTowerData = towerPrefab;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void UpgradeTower()
    {   
        int upPrice = cloneTower.gameObject.GetComponent<Tower>().GetUpgradePath().Item1;
        // int upPrice = cloneTowerData.GetUpgradePath().Item1;
        if(PlayerStats.TP < upPrice)
        {
            return;
        }
        
        PlayerStats.TP -= upPrice;

        // Remove old tower
        Destroy(cloneTower);
        
        // Building upgraded tower
        cloneTower = (GameObject)Instantiate(cloneTowerData.GetUpgradePath().Item2.gameObject, GetBuildPosition(), Quaternion.identity);
        
        // This gets the next upgrade for the tower
        cloneTowerData = cloneTowerData.GetUpgradePath().Item2;  

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void SellTower()
    {
        PlayerStats.TP += cloneTower.GetComponent<Tower>().GetSellPrice();
        Debug.Log("sold for " + cloneTower.GetComponent<Tower>().GetSellPrice());

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Destroy(cloneTower);
        cloneTower = null;
    }

    // OnMoustEnter (mouse move in) to change corresponding color of node
    void OnMouseEnter() 
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(!buildManager.CanBuild)
            return;
        
        // Changes color of node if player has TP or if there's a 'type' mismatch
        if (!buildManager.HasMoney || !(buildManager.IsMPTower == this.forMPTowers))
            rend.material.color = errorColor;
        else
            rend.material.color = hoverColor;
    }

    // OnMouseExit (mouse not hovering) reverts the node to its starting color
    void OnMouseExit() 
    {
        rend.material.color = startColor;
    }

    // OnMouseDown (mouse click) to do specific actions
    void OnMouseDown() 
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if(cloneTower != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.CanBuild)
            return;

        // Normal Tower and Normal Node
        if(!this.forMPTowers && !buildManager.IsMPTower)
            BuildTower(buildManager.GetTowerToBuild());

        // MPTower and MP Node
        else if (this.forMPTowers && buildManager.IsMPTower)
            BuildTower(buildManager.GetTowerToBuild());

        // Tower and Node mismatch
        else
            Debug.Log("TOWER AND NODE TYPE MISMATCH");
    }

}
