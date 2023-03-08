using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : MonoBehaviour
{
    // ShopPage is the script that handles sending the 
    // tower prefab data to the BuildManager. 
    // For this to work, the tower prefab must be assigned in the
    // Unity Editor with the appropriate tower.
    
    BuildManager buildManager;
    Loadout loadout;
    // HoverUI hoverUI;

    List<Tower> currentLoadout;

    [SerializeField] GameObject shopButtonPrefab;
    [SerializeField] GameObject buttonParent;


    void Awake()
    {
        buildManager = BuildManager.instance;
        loadout = Loadout.loadoutInstance;
        // hoverUI = HoverUI.hoverUIInstance;
    }
    
    void OnEnable()    
    {   
        if (Loadout.savedLoadout.Count < Loadout.LoadoutCount || Loadout.savedLoadout == null)
        {
            currentLoadout = loadout.defaultLoadout;
            Debug.Log("DEFAULT LOADOUT LOADED");
        }
        else
        {
            currentLoadout = Loadout.savedLoadout;
            Debug.Log("CUSTOM SAVED LOADOUT LOADED");
        }
            
        foreach (Tower _tower in currentLoadout)
        {
            GameObject newButton = Instantiate(shopButtonPrefab, buttonParent.transform);
            newButton.GetComponent<ShopButton>().assignedTower = _tower;
            newButton.GetComponent<Button>().onClick.AddListener(() => BuildTowerButton(newButton.GetComponent<Button>()));
        }
    }

    public void BuildTowerButton(Button _button)
    {
        buildManager.SelectTowerToBuild(_button.GetComponent<ShopButton>().assignedTower);
        HoverUI.hoverUIInstance.Activate(_button.GetComponent<ShopButton>().assignedTower);  // Shows sprite & range of selected tower
    }
}
