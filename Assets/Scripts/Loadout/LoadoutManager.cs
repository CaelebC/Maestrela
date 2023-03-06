using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    // public static LoadoutManager instance;
    
    private int maxCountLoadout = 8;
    [SerializeField] private List<Tower> allTowersList;
    [SerializeField] private List<Tower> defaultLoadoutList;
    [HideInInspector] public List<Tower> savedLoadout = new List<Tower>();
    private List<Tower> tempLoadout = new List<Tower>();

    [Header("Unity Setup Fields")]
    public LoadoutButton[] buttonPool;
    public GameObject buttonContainer;
    public DescriptionPanel descPanel;

    public static event Action<List<Sprite>> RefreshLoadoutSprites;

    
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
        }
    }

    public void UpdateDescription(Tower _tower)
    {
        descPanel.UpdateTowerData(_tower);
    }

    void AddToLoadout(Tower _tower)
    {
        if (tempLoadout.Count < maxCountLoadout)
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

    void SaveLoadout()
    {
        savedLoadout = tempLoadout;
        Debug.Log("loadout saved successfully. savedLoadout.Count: " + savedLoadout.Count);
    }

    void ClearLoadout()
    {
        tempLoadout.Clear();
        ShowCardsSprites();
    }

    void UseDefaultLoadout()
    {
        tempLoadout = defaultLoadoutList;
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
