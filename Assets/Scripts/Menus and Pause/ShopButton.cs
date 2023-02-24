using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    [Header("Setup Fields")]
    public Tower assignedTower;
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerPrice;
    public Image towerSprite;


    void Start()
    {
        towerName.text = assignedTower.towerName;
        towerPrice.text = "TP " + assignedTower.Price.ToString();
        towerSprite.sprite = assignedTower.towerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
