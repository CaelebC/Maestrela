using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Tower assignedTower;
    
    [Header("Unity Setup Fields")]
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerPrice;
    public Image towerSprite;

    public Button thisButtonRef;
    public Image cooldownOverlay;

    [HideInInspector] public bool onCooldown = false;
    private float tempCooldownTimer;
    // public bool OnCooldown{ get{return onCooldown;} }

    void Start()
    {
        towerName.text = assignedTower.towerName;
        towerPrice.text = "TP " + assignedTower.Price.ToString();
        towerSprite.sprite = assignedTower.towerSprite;
    }

    void Update()
    {
        // CheckCooldown();
    }

    void CheckCooldown()
    {
        if (!onCooldown)
        {
            Debug.Log("not onCooldown");
            thisButtonRef.interactable = true;
            onCooldown = true;
            cooldownOverlay.fillAmount = 1f;
        }

        // Tower was purchased, now on cooldown
        if (onCooldown)
        {
            Debug.Log("NAKA CD LODS");
            Debug.Log(assignedTower.BuyCooldown);
            thisButtonRef.interactable = false;

            cooldownOverlay.fillAmount -= 1f / (assignedTower.BuyCooldown * Time.deltaTime);

            if (cooldownOverlay.fillAmount <= 0f)
            {
                cooldownOverlay.fillAmount = 0f;
                onCooldown = false;
            }
        }

    }
}
