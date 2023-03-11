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
    [SerializeField] TextMeshProUGUI towerName;
    [SerializeField] TextMeshProUGUI towerPrice;
    [SerializeField] Image towerSprite;

    [SerializeField] Button thisButtonRef;
    [SerializeField] Image cooldownOverlay;

    [HideInInspector] public bool onCooldown = false;
    private float tempCooldownTimer;

    void Start()
    {
        buildManager = BuildManager.instance;

        towerName.text = assignedTower.towerName;
        towerPrice.text = "TP " + assignedTower.Cost.ToString();
        towerSprite.sprite = assignedTower.towerSprite;

        tempCooldownTimer = assignedTower.BuyCooldown;
        thisButtonRef = this.GetComponent<Button>();

        Node.onTowerBuilt += CheckCooldown;
    }

    void OnDestroy() 
    {
        Node.onTowerBuilt -= CheckCooldown;
    }

    void Update()
    {
        ShowCooldown();
    }

    void CheckCooldown(Tower _towerData)
    {
        // If the tower built is the same as the button's assigned tower,
        // then the cooldown will be set to true.
        if (_towerData == assignedTower)
        {
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
            tempCooldownTimer -= Time.deltaTime;
            thisButtonRef.interactable = false;
            cooldownOverlay.fillAmount = tempCooldownTimer / assignedTower.BuyCooldown;

            if (tempCooldownTimer <= 0f)
            {
                onCooldown = false;
                thisButtonRef.interactable = true;
                tempCooldownTimer = assignedTower.BuyCooldown;
            }
        }
    }
}
