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


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        loadoutManager.UpdateDescription(towerData);
    }
}
