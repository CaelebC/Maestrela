using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    // public static LoadoutManager instance;
    
    [SerializeField] private List<Tower> allTowersList;
    [HideInInspector] public List<Tower> savedLoadout;
    private List<Tower> tempLoadout;

    [Header("Unity Setup Fields")]
    // public GameObject buttonPrefab;
    public LoadoutButton[] buttonPool;
    public GameObject buttonContainer;
    public DescriptionPanel descPanel;

    
    void OnEnable()    
    {
        int i = 0;
        foreach (Tower _tower in allTowersList)
        {
            buttonPool[i].gameObject.SetActive(true);
            buttonPool[i].loadoutManager = this;
            buttonPool[i].towerData = _tower;
            buttonPool[i].towerName.text = _tower.towerName;
            buttonPool[i].towerType.text = _tower.towerProjectileType.ToString();
            buttonPool[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttonPool[i].GetComponent<Button>().onClick.AddListener(() => AddToLoadout(_tower));
            
            i += 1;
            // GameObject newButton = Instantiate(buttonPrefab, buttonContainer.transform);
            // newButton.GetComponent<LoadoutButton>().towerData = _tower;
        }
        
    }

    public void AddToLoadout(Tower _tower)
    {
        Debug.Log(_tower.towerName + " was added");
        tempLoadout.Add(_tower);
    }

    public void UpdateDescription(Tower _tower)
    {
        descPanel.UpdateTowerData(_tower);
    }

    public void SaveLoadout()
    {
        savedLoadout = tempLoadout;
        Debug.Log("loadout saved successfully");
        Debug.Log(savedLoadout.Count);
    }

    public void ClearLoadout(Tower _selectedTowerData)
    {
        tempLoadout.Clear();
    }
}
