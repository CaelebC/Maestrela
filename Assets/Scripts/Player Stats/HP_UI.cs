using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_UI : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    // Update is called once per frame
    void Update()
    {
        hpText.text = PlayerStats.HP + " HP";
    }
}
