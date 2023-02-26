using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSpaceNum_UI : MonoBehaviour
{
    public TextMeshProUGUI towerSpaceNumText;
    
    // Update is called once per frame
    void Update()
    {
        towerSpaceNumText.text = "Space: " + (PlayerStats.maxTowerSpace - PlayerStats.numBuiltTowers).ToString();
    }
}
