using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveNumUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;

    void Update()
    {
        waveText.text = "WAVE " + PlayerStats.waves.ToString();
    }
}

