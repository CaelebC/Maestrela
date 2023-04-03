using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    [HideInInspector] public Tower towerData;
    [HideInInspector] public MPTower mpTowerData;
    [HideInInspector] private bool isItAnMPTower;
    private bool uiDisplaying = false;
    private string hexTextColor;

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
        // UpdateDescription();
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

        // Actually updating the description panel already
        hexTextColor = "";
        if (!isItAnMPTower) { UpdateDMGDescription(); }
        else if (isItAnMPTower) { UpdateMPDescription(); }
    }

    void UpdateDMGDescription()
    {
        hexTextColor = EntityTypeColor.TypeColor(towerData.TowerEntityType);
        
        towerImage.sprite = towerData.towerSprite;
        towerName.text = towerData.towerName;

        tpCost.text = "TP Cost: " + towerData.Cost.ToString();
        deployCD.text = "Deploy Cooldown: " + towerData.BuyCooldown.ToString();

        damage_hp.text = "Damage: " + towerData.Damage.ToString() + " " + towerData.TowerAttackType.ToString();
        fireRate_regenAmt.text = "Fire Rate: " + towerData.FireRate.ToString();
        range_vanish.text = "Range: " + RangeText.GetRange(towerData.Range);
        type_effect.text = "Type: <color="+hexTextColor+">" + towerData.TowerEntityType.ToString() + "</color>";
    }

    void UpdateMPDescription()
    {        
        hexTextColor = EntityTypeColor.TypeColor(mpTowerData.TowerEntityType);
        
        towerImage.sprite = mpTowerData.towerSprite;
        towerName.text = mpTowerData.towerName;

        tpCost.text = "TP Cost: " + mpTowerData.Cost.ToString();
        deployCD.text = "Deploy Cooldown: " + mpTowerData.BuyCooldown.ToString();
        
        damage_hp.text = "Tower HP: " + mpTowerData.TowerHP.ToString();
        fireRate_regenAmt.text = "MP Regenerated: " + (mpTowerData.RegenMPPercent * 100).ToString() + "%";
        range_vanish.text = "Waves to Vanish: " + mpTowerData.VanishAfterWaves.ToString();

        type_effect.text = "Effect: <color="+hexTextColor+">" + (mpTowerData.TowerEffect.Item1 * 100).ToString() + "% " + mpTowerData.TowerEffect.Item2 + "</color>";
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
