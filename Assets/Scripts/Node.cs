using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    private GameObject tower;
    public Vector3 positionOffset;

    BuildManager buildManager;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    
    void OnMouseDown() 
    {
        if(buildManager.GetTowerToBuild() == null)
            return;
        
        if(tower != null)
        {
            Debug.Log("Something there already");
            return;
        }

        // Build a tower
        GameObject towerToBuild = buildManager.GetTowerToBuild();
        tower = (GameObject)Instantiate(towerToBuild, (transform.position + positionOffset), transform.rotation);
    }

    void OnMouseEnter() 
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(buildManager.GetTowerToBuild() == null)
            return;
        
        rend.material.color = hoverColor;
    }

    void OnMouseExit() 
    {
        rend.material.color = startColor;
    }
}
