using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NodeUI : MonoBehaviour
{
    // NodeUI handles the upgrade and sell buttons that appear a specific
    // tower. This gets the data from the specific Node that was selected.
    
    public GameObject ui;

    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerLevel;

    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    public TextMeshProUGUI sellCost;

    private Node target;
    private Tower towerOnTarget;

    void Update()
    {
        CheckTPForUpgrade();
    }

    public void SetTarget(Node _target)
    {
        this.target = _target;
        towerOnTarget = target.cloneTower.GetComponent<Tower>();
        transform.position = target.GetBuildPosition();

        // This is for changing NodeUI content
        towerName.text = towerOnTarget.towerName;
        CheckUpgrades(target);
        sellCost.text = "TP " + towerOnTarget.GetSellPrice().ToString();
        towerLevel.text = "Level " + towerOnTarget.currentUpgradeLevel;
        towerOnTarget.ToggleTowerRange();

        ui.SetActive(true);
    }

    public void HideNodeUI()
    {
        if (target)
        {
            towerOnTarget.ToggleTowerRange();
            target = null;
        }
        
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        target = null;
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        target = null;
        BuildManager.instance.DeselectNode();
    }

    public void CheckUpgrades(Node _target)
    {
        // Getting upgradeability/price of upgrade
        if (!_target.cloneTowerData.IsUpgradeable)
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "MAX";
            return;
        }
        else
        {
            upgradeCost.text = "TP " + towerOnTarget.GetUpgradePath().Item1.ToString();
            CheckTPForUpgrade();
        }
    }

    void CheckTPForUpgrade()
    {
        if (!target || upgradeCost.text == "MAX")
        {
            return;
        }
        
        // Changes button interactability depending on if player has money for upgrade
        if (towerOnTarget.GetUpgradePath().Item1 > PlayerStats.TP)
        {
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }
}
