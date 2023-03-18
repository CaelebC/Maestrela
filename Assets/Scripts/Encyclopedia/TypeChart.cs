using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeChart : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    [SerializeField] GameObject typeChartPanel;
    
    public void ShowTypeChart()
    {
        typeChartPanel.SetActive(true);
    }

    public void HideTypeChart()
    {
        typeChartPanel.SetActive(false);
    }
}
