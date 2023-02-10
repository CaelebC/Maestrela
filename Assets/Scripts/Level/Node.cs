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
    
    public Color hoverColor;
    public Color insufficientCostColor;
    private Color startColor;
    public Color mptColor;

    public Vector3 towerPositionOffset;

    private Renderer rend;  // Used for modifying Node GameObject's color

    [HideInInspector] public GameObject tower;
    [HideInInspector] public TowerBlueprint towerBlueprint;

    [HideInInspector] public bool isUpgraded = false;
    public bool forMPTowers;


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

    void BuildTower(TowerBlueprint blueprint)
    {
        if(PlayerStats.TP < blueprint.cost)
        {
            // Debug.Log("not enough TP");
            return;
        }

        PlayerStats.TP -= blueprint.cost;
        
        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;
        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        // Debug.Log("tower built");
    }

    public void UpgradeTower()
    {
        if(PlayerStats.TP < towerBlueprint.upgradeCost)
        {
            // Debug.Log("not enough TP for upgrade");
            return;
        }

        PlayerStats.TP -= towerBlueprint.upgradeCost;

        // Remove old tower
        Destroy(tower);
        
        // Building upgraded tower
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void SellTower()
    {
        PlayerStats.TP += towerBlueprint.GetSellPrice();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Destroy(tower);
        towerBlueprint = null;
    }

    // OnMoustEnter (mouse move in) to change corresponding color of node
    void OnMouseEnter() 
    {
        
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(!buildManager.CanBuild)
            return;

        // if(this.forMPTowers)
        //     return;
        
        if (!buildManager.HasMoney)
        {
            rend.material.color = insufficientCostColor;
        }
        else if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        if (buildManager.IsMPTower)
        {
            rend.material.color = mptColor;
        }
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
        
        if(tower != null)
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
