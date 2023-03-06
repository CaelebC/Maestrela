using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LoadoutButton : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector] public LoadoutManager loadoutManager;
    [HideInInspector] public Tower towerData;
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerType;


    void Start()
    {
        // towerName.text = towerData.towerName;
        // towerType.text = towerData.towerProjectileType.ToString();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        loadoutManager.UpdateDescription(towerData);
        Debug.Log("YOUR MOUSE IS ON THIS BUTTON: " + towerName);
    }

}
