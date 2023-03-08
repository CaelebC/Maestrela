using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    // Node handles all the code that relates to the 
    // building, selling, and upgrading of towers. 
    // This is also where the detection for 
    // mouse clicks, hovers, and leaves is handled. 
    
    BuildManager buildManager;
    HoverUI hoverUI;
    
    public bool forMPTowers;

    public Vector3 towerPositionOffset;

    private Color startColor;  // This is to store the 'original' color of the node, allowing the color to revert to the original after being changed a bunch.
    public Color hoverColor;
    public Color errorColor;
    
    private Renderer rend;  // Used for modifying Node GameObject's color

    [HideInInspector] public GameObject cloneTower;
    [HideInInspector] public Tower cloneTowerData;

    public static event Action<Tower> onTowerBuilt;  // Event broadcaster instantiation


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        hoverUI = HoverUI.hoverUIInstance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + towerPositionOffset;
    }

    void BuildTower(Tower towerPrefab)
    {
        if(PlayerStats.TP < towerPrefab.Price)
        {
            return;
        }

        PlayerStats.TP -= towerPrefab.Price;
        hoverUI.Deactivate();  // Deactivates the tower hover UI
        
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
        // Debug.Log("sold for " + cloneTower.GetComponent<Tower>().GetSellPrice());

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

        // TODO: Have an error sound effect and show why there's an error
        // Changes color of node if player has TP
        if (!buildManager.HasMoney)
            rend.material.color = errorColor;
        
        // Changes color of node if there's a 'type' mismatch
        else if (!(buildManager.IsMPTower == this.forMPTowers))
            rend.material.color = errorColor;

        // // Changes color of node if max number of towers is reached
        // else if (!buildManager.ReachedMaxTowerSpace)
        //     rend.material.color = errorColor;

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
            hoverUI.Deactivate();
            return;
        }
        
        // This runs if a tower is already placed, and will then show the NodeUI.
        if(cloneTower != null)
        {
            hoverUI.Deactivate(); 
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.CanBuild)
        {
            return;
        }
            

        if (buildManager.AtMaxTowerSpace)
        {
            return;
        }
            

        // Build Normal Tower and Normal Node || Build MPTower and MP Node
        if( (!this.forMPTowers && !buildManager.IsMPTower) || (this.forMPTowers && buildManager.IsMPTower) )
        {
            BuildTower(buildManager.GetTowerToBuild());
            onTowerBuilt?.Invoke(cloneTowerData);
            buildManager.DeselectNodeAfterBuild();
        }

        // Tower and Node mismatch
        else
        {
            // TODO: Have an error sound effect and show text that it's a tower&node type mismatch.
            Debug.Log("TOWER AND NODE TYPE MISMATCH");
        }
            
    }

}
