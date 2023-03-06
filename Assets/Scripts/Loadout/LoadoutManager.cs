using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    // public static LoadoutManager instance;
    
    public List<Tower> savedLoadout;
    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    public DescriptionPanel descPanel;

    private void Awake() 
    {
        
    }
    
    void OnEnable()    
    {
        // for (int i = 0; i < )
        // GameObject newButton = Instantiate(buttonPrefab, buttonContainer, buttonContainer.transform);
    }

    void Update() 
    {
        
    }

    public static void UpdateDescription(Tower _tower)
    {
        // descPanel.towerData = _tower;
    }

    public void SaveLoadout()
    {
        // PlayerStats.towerLoadout = savedLoadout;
    }

    public void Add()
    {
        // pStats.tempTester += "wow this REALLY ACTUALLY works ";
    }

    public void Remove(Tower _selectedTowerData)
    {
        // towersInLoadout.Remove(_selectedTowerData);
    }
}
