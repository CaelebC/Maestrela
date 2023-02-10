using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveNum : MonoBehaviour
{
    public TextMeshProUGUI tpText;
    
    // Update is called once per frame
    void Update()
    {
        tpText.text = "WAVE " + PlayerStats.waves.ToString();
    }
}

