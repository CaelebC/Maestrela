using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{    
    Loadout loadout;

    private List<Tower> tempLoadout = new List<Tower>();

    [Header("Unity Setup Fields")]
    public LoadoutButton[] buttonPool;
    public GameObject buttonContainer;
    public DescriptionPanel descPanel;

    public static event Action<List<Sprite>> RefreshLoadoutSprites;

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
            Loadout.savedLoadout = tempLoadout;
            Debug.Log("loadout saved successfully. savedLoadout.Count: " + Loadout.savedLoadout.Count);
        }
        else 
        {
            // TODO: TEXT NOTIFICATION STATING THAT YOU NEED 8 TOWERS TO SAVE
            Debug.Log("SAVE FAILED, DO AGAIN NEED 8");
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
        // Event system to send sprites to display selected cards
        // The ?.Invoke is a null checker. If it isn't null, then the Invoke() will be 'ran'.
        RefreshLoadoutSprites?.Invoke(towerSprites);
    }
}
