using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MP_UI : MonoBehaviour
{
    public TextMeshProUGUI mpText;

    // Update is called once per frame
    void Update()
    {
        mpText.text = "MP " + Mathf.Floor(PlayerStats.MP) + "/" + Mathf.Floor(PlayerStats.maxMP);
    }
}
