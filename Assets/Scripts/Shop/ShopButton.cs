using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    [HideInInspector] public Tower assignedTower;
    [HideInInspector] public string typeHexColor;
    [HideInInspector] public Color towerGlowColor;
    private float fadedGlowAlpha = 0.25f;

    BuildManager buildManager;
    
    [Header("Unity Setup Fields")]
    [SerializeField] TextMeshProUGUI towerName;
    [SerializeField] TextMeshProUGUI towerPrice;
    [SerializeField] Image towerSprite;
    [SerializeField] Image towerSpriteBG;

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

        typeHexColor = EntityTypeColor.TypeColor(assignedTower.TowerEntityType);
        ColorUtility.TryParseHtmlString(typeHexColor, out towerGlowColor);
        towerSpriteBG.sprite = assignedTower.towerSprite;
        towerSpriteBG.color = towerGlowColor;

        tempCooldownTimer = assignedTower.BuyCooldown;
        thisButtonRef = this.GetComponent<Button>();

        Node.OnTowerBuilt += CheckCooldown;
    }

    void OnDestroy() 
    {
        Node.OnTowerBuilt -= CheckCooldown;
    }

    void Update()
    {
        CheckPlayerTP();
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
            towerSpriteBG.canvasRenderer.SetAlpha(fadedGlowAlpha);

            if (tempCooldownTimer <= 0f)
            {
                onCooldown = false;
                thisButtonRef.interactable = true;
                towerSpriteBG.canvasRenderer.SetAlpha(1);
                tempCooldownTimer = assignedTower.BuyCooldown;
            }
        }
    }

    void CheckPlayerTP()
    {
        if (assignedTower.Cost > PlayerStats.TP)
        {
            thisButtonRef.interactable = false;
            towerSpriteBG.canvasRenderer.SetAlpha(fadedGlowAlpha);
        }
        else if (!onCooldown && !buildManager.AtMaxTowerSpace)
        {
            thisButtonRef.interactable = true;
            towerSpriteBG.canvasRenderer.SetAlpha(1);
        }
    }
}
