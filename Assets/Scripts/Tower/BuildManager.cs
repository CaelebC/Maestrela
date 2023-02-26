using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    // BuildManager handles the selecting of the Node where a tower
    // is planned to be built on. 
    // The BuildManager is the 'brain' of being able to place towers
    // on the nodes in the level.
    
    public static BuildManager instance;

    private Tower towerToBuild;
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
    public bool IsMPTower{ get{return towerToBuild.IsMPTower;} }
    public bool HasMoney{ get{return PlayerStats.TP >= towerToBuild.Price;} }

    public void SelectTowerToBuild(Tower tower)
    {
        towerToBuild = tower;
        DeselectNode();
    }

    public Tower GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void SelectNode(Node _node)
    {
        if (selectedNode == _node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = _node;
        towerToBuild = null;

        nodeUI.SetTarget(_node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideNodeUI();
    }

    public void DeselectNodeAfterBuild()
    {
        towerToBuild = null;
    }
}
