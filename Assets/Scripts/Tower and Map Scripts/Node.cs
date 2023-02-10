using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientCostColor;
    private Color startColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject tower;
    private Renderer rend;
    public TowerBlueprint towerBlueprint;
    public bool isUpgraded = false;
    BuildManager buildManager;



    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // TODO: Replace all OnMouse functions to work for mobile interface
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

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint blueprint)
    {
        if(PlayerStats.TP < blueprint.cost)
        {
            Debug.Log("not enough TP");
            return;
        }

        PlayerStats.TP -= blueprint.cost;
        
        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;
        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        Debug.Log("tower built");
    }

    public void UpgradeTower()
    {
        if(PlayerStats.TP < towerBlueprint.upgradeCost)
        {
            Debug.Log("not enough TP for upgrade");
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

    void OnMouseEnter() 
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(!buildManager.CanBuild)
            return;
        
        rend.material.color = buildManager.HasMoney ? hoverColor : insufficientCostColor;
    }

    void OnMouseExit() 
    {
        rend.material.color = startColor;
    }
}
