using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_UI : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    private float startingHP;
    [SerializeField] Image imageBar;

    void Start() 
    {
        startingHP = (float)PlayerStats.HP;
        // Debug.Log(startingHP);
    }

    void Update()
    {
        hpText.text = "HP " + PlayerStats.HP;
        imageBar.fillAmount = (float)PlayerStats.HP / startingHP;
    }
}
