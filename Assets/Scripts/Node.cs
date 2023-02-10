using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientCostColor;
    private Renderer rend;
    private Color startColor;

    public Vector3 positionOffset;

    BuildManager buildManager;

    [Header("Optional")]
    public GameObject tower;

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
        if(!buildManager.CanBuild)
            return;
        
        if(tower != null)
        {
            Debug.Log("Something there already");
            return;
        }

        buildManager.BuildTowerOn(this);
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
