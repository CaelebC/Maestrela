using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{    
    Loadout loadout;

    private List<Tower> tempLoadout = new List<Tower>();
    string alreadyInLoadoutMSG = "Tower is already in loadout";
    string loadoutSavedMSG = "Loadout Saved";
    string lessThanRequiredTowersMSG = Loadout.LoadoutCount + " towers needed to save loadout";

    [Header("Unity Setup Fields")]
    public LoadoutButton[] buttonPool;
    public GameObject buttonContainer;
    public DescriptionPanel descPanel;

    public static event Action<List<Sprite>> RefreshLoadoutSprites;
    public static event Action<string> GiveNotif;

    void Awake()
    {
        loadout = Loadout.loadoutInstance;
    }
    
    void OnEnable()    
    {
        int i = 0;
        foreach (Tower _tower in loadout.allTowers)
        {
            buttonPool[i].gameObject.SetActive(true);

            buttonPool[i].loadoutManager = this;
            buttonPool[i].towerData = _tower;
            buttonPool[i].towerName.text = _tower.towerName;
            buttonPool[i].towerType.text = _tower.towerProjectileType.ToString();

            buttonPool[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttonPool[i].GetComponent<Button>().onClick.AddListener(() => AddToLoadout(_tower));
            
            i += 1;
        }
    }

    public void UpdateDescription(Tower _tower)
    {
        descPanel.UpdateTowerData(_tower);
    }

    void AddToLoadout(Tower _tower)
    {
        if (tempLoadout.Contains(_tower))
        {
            GiveNotif?.Invoke(alreadyInLoadoutMSG);
            return;
        }
        
        if (tempLoadout.Count < Loadout.LoadoutCount)
        { 
            tempLoadout.Add(_tower); 
        }
        else
        {
            tempLoadout.RemoveAt(0);
            tempLoadout.Add(_tower);
        }
        ShowCardsSprites();
    }

    public void SaveLoadout()
    {
        if (tempLoadout.Count == Loadout.LoadoutCount)
        {
            GiveNotif?.Invoke(loadoutSavedMSG);
            Loadout.savedLoadout = tempLoadout;
        }
        else 
        {
            GiveNotif?.Invoke(lessThanRequiredTowersMSG);
        }
        
    }

    public void ClearLoadout()
    {
        tempLoadout.Clear();
        ShowCardsSprites();
    }

    public void UseDefaultLoadout()
    {
        tempLoadout = loadout.defaultLoadout;
        ShowCardsSprites();
    }

    public void ShowCardsSprites()
    {
        List<Sprite> towerSprites = new List<Sprite>();
        
        foreach(Tower _tower in tempLoadout)
        {
            towerSprites.Add(_tower.towerSprite);
        }
        RefreshLoadoutSprites?.Invoke(towerSprites);
    }
}
