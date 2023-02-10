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

    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    public TextMeshProUGUI sellCost;

    private Node target;

    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        // This is for changing the buttons' text
        CheckUpgrades(target);
        sellCost.text = "TP " + target.cloneTowerData.GetSellPrice();

        ui.SetActive(true);
    }

    public void HideNodeUI()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
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
            upgradeCost.text = "TP " + _target.cloneTowerData.GetUpgradePath().Item1.ToString();
        }
    }
}
