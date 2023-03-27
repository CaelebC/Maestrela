using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MP_UI : MonoBehaviour
{
    public TextMeshProUGUI mpText;
    [SerializeField] Image imageBar;


    void Update()
    {
        mpText.text = "MP " + Mathf.Floor(PlayerStats.MP);
        imageBar.fillAmount = (PlayerStats.MP / PlayerStats.maxMP);
    }
}
