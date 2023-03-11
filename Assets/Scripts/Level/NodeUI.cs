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
        if (!_target.cloneTowerData.IsUpgradeable)
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "MAX";
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "TP " + towerOnTarget.GetUpgradePath().Item1.ToString();
        }
    }
}
