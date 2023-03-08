using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Tower assignedTower;

    BuildManager buildManager;
    
    [Header("Unity Setup Fields")]
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerPrice;
    public Image towerSprite;

    public Button thisButtonRef;
    public Image cooldownOverlay;

    [HideInInspector] public bool onCooldown = false;
    private float tempCooldownTimer;

    void Start()
    {
        buildManager = BuildManager.instance;

        towerName.text = assignedTower.towerName;
        towerPrice.text = "TP " + assignedTower.Cost.ToString();
        towerSprite.sprite = assignedTower.towerSprite;

        tempCooldownTimer = assignedTower.BuyCooldown;
        Node.onTowerBuilt += CheckCooldown;

    }

    void OnDestroy() 
    {
        Node.onTowerBuilt -= CheckCooldown;
    }

    void Update()
    {
        ShowCooldown();
        CheckTowerSpace();
    }

    void CheckCooldown(Tower _towerData)
    {
        // If the tower built is the same as the button's assigned tower,
        // then the cooldown will be set to true.
        if (_towerData == assignedTower)
        {
            // Debug.Log("_towerData:" + _towerData.towerName);
            onCooldown = true;
            cooldownOverlay.fillAmount = 1f;
        }
    }

    void ShowCooldown()
    {
        // Tower was purchased, now showing cooldown anim
        // button will also be non interactable.
        if (onCooldown)
        {
            // Debug.Log("TOWER ON CD");
            tempCooldownTimer -= Time.deltaTime;
            
            thisButtonRef.interactable = false;
            cooldownOverlay.fillAmount = tempCooldownTimer / assignedTower.BuyCooldown;

            if (tempCooldownTimer <= 0f)
            {
                onCooldown = false;
                tempCooldownTimer = assignedTower.BuyCooldown;
            }
        }

    }

    void CheckTowerSpace() => thisButtonRef.interactable = buildManager.AtMaxTowerSpace ? false : true;
}
