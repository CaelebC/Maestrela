using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EncyclButton : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector] public EncyclManager encyclManager;
    [HideInInspector] public Tower towerData;
    [HideInInspector] public Enemy enemyData;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (towerData)
        {
            encyclManager.UpdateDescription(towerData);
            return;
        }

        if (enemyData)
        {
            encyclManager.UpdateEnemyDescription(enemyData);
            return;
        }
        
    }
}
