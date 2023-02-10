using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public GameObject ui;
    
    public CardSO cardSelected;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public Image cardSprite;

    void Start()
    {
        cardName.text = cardSelected.cardName;
        cardDescription.text = cardSelected.cardDescription;
        cardSprite.sprite = cardSelected.cardSprite;
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1f;
        }
    }

}
