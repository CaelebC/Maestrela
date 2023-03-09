using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TP_UI : MonoBehaviour
{
    public TextMeshProUGUI tpText;
    
    // Update is called once per frame
    void Update()
    {
        tpText.text = "TP " + PlayerStats.TP.ToString() + "/" + PlayerStats.maxTP.ToString();
    }
}
