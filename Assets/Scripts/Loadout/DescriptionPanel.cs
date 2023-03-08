using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    public Tower towerData;
    public MPTower mpTowerData;
    private bool isItAnMPTower;
    private bool uiDisplaying = false;

    [Header("Unity Inspector Assignments")]
    public Image towerImage;
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI tpCost;
    public TextMeshProUGUI deployCD;
    public TextMeshProUGUI damage_hp;  // _ is to denote an or symbol in this context. || couldn't be used
    public TextMeshProUGUI fireRate_regenAmt;
    public TextMeshProUGUI range_vanish;
    public TextMeshProUGUI type_effect;

    void Start()
    {
        UpdateDescription();
    }
    
    public void UpdateTowerData(Tower _tower)
    {
        isItAnMPTower = _tower.GetComponent<Tower>().IsMPTower;
        if (!isItAnMPTower) 
        { 
            towerData = _tower.GetComponent<Tower>(); 
            mpTowerData = null;
        }
        else 
        {  
            towerData = null; 
            mpTowerData = _tower.GetComponent<MPTower>();
        }

        UpdateDescription();
    }

    void UpdateDescription()
    {
        if (!isItAnMPTower)
        {
            towerImage.sprite = towerData.towerSprite;
            towerName.text = towerData.towerName;

            tpCost.text = "TP Cost: " + towerData.Cost.ToString();
            deployCD.text = "Deploy Cooldown: " + towerData.BuyCooldown.ToString();

            damage_hp.text = "Damage: " + towerData.Damage.ToString();
            fireRate_regenAmt.text = "Fire Rate: " + towerData.FireRate.ToString();
            range_vanish.text = "Range: " + towerData.Range.ToString();
            type_effect.text = "Type: " + towerData.TowerEntityType.ToString();
        }
        else 
        {
            towerImage.sprite = mpTowerData.towerSprite;
            towerName.text = mpTowerData.towerName;

            tpCost.text = "TP Cost: " + mpTowerData.Cost.ToString();
            deployCD.text = "Deploy Cooldown: " + mpTowerData.BuyCooldown.ToString();
            
            damage_hp.text = "Tower HP: " + mpTowerData.TowerHP.ToString();
            fireRate_regenAmt.text = "MP Regenerated: " + mpTowerData.RegenMPPercent.ToString() + "%";
            range_vanish.text = "Waves to Vanish: " + mpTowerData.VanishAfterWaves.ToString();

            type_effect.text = "Effect: " + mpTowerData.TowerEffect.Item1.ToString() + "% " + mpTowerData.TowerEffect.Item2;
        }
    }

    public void TogglePanelItems()
    {
        if (uiDisplaying)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var child = this.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var child = this.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(true);
            }
        }
    }
}
