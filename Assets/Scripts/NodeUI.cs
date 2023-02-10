using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI sellCost;
    public Button upgradeButton;


    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();


        if (!target.isUpgraded)
        {
            upgradeCost.text = "TP " + target.towerBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellCost.text = "TP " + target.towerBlueprint.GetSellPrice();

        ui.SetActive(true);
    }

    public void HideNodeUI()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("YOU PRESSED UPGRADE BUTTON");
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        Debug.Log("SELL BUTTON PRESSED");
        target.SellTower();
        target.isUpgraded = false;
        BuildManager.instance.DeselectNode();
    }
}
