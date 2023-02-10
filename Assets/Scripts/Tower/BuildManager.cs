using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // BuildManager handles the selecting of the Node where a tower
    // is planned to be built on. 
    // The BuildManager is the 'brain' of being able to place towers
    // on the nodes in the level.
    
    public static BuildManager instance;

    private TowerBlueprint towerToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;


    void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 build manager in scene");
            return;
        }
        
        instance = this;
    }

    public bool CanBuild{ get{return towerToBuild != null;} }
    public bool IsMPTower{ get{return towerToBuild.isMPTower;} }
    public bool HasMoney{ get{return PlayerStats.TP >= towerToBuild.cost;} }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideNodeUI();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
