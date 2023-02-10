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
    
    private Color startColor;  // This is to store the 'original' color of the node, allowing the color to revert to the original after being changed a bunch.
    public Color hoverColor;
    public Color errorColor;

    public Vector3 towerPositionOffset;

    private Renderer rend;  // Used for modifying Node GameObject's color

    [HideInInspector] public Tower tower;
    // [HideInInspector] public Tower towerBlueprint;

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

    void BuildTower(Tower blueprint)
    {
        if(PlayerStats.TP < blueprint.price)
        {
            // Debug.Log("not enough TP");
            return;
        }

        PlayerStats.TP -= blueprint.price;
        
        Tower _tower = Instantiate(blueprint, GetBuildPosition(), Quaternion.identity);
        tower = _tower;
        // towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        // Debug.Log("tower built");
    }

    public void UpgradeTower()
    {
        if(PlayerStats.TP < tower.GetUpgradePath().Item1)
        {
            // Debug.Log("not enough TP for upgrade");
            return;
        }

        PlayerStats.TP -= tower.GetUpgradePath().Item1;

        // Remove old tower
        Destroy(tower);
        
        // Building upgraded tower
        Tower _tower = Instantiate(tower.GetUpgradePath().Item2, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void SellTower()
    {
        PlayerStats.TP += tower.GetSellPrice();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Destroy(tower);
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
