using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TowerBlueprint towerToBuild;
    public GameObject tower1Prefab;
    public GameObject tower2Prefab;


    void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 build manager in scene");
            return;
        }
        
        instance = this;
    }

    // void Start() 
    // {
    //     towerToBuild = standardTowerPrefab;
    // }

    public bool CanBuild{ get{return towerToBuild != null;} }

    public void BuildTowerOn(Node node)
    {
        if(PlayerStats.TP < towerToBuild.cost)
        {
            Debug.Log("not enough TP");
            return;
        }

        PlayerStats.TP -= towerToBuild.cost;
        
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.tower = tower;

        Debug.Log("tower built. TP left: " + PlayerStats.TP);
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }
}
