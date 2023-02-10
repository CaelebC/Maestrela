using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCardsUI : MonoBehaviour
{
    public GameObject cardSpritePrefab;
    public Transform parent;
    
    void Awake() 
    {
        // Event handler for card being selected
        // CardManager cardManager = GetComponent<CardManager>();
        CardManager.ShowSpritesOnApply += UpdateSelectedCardsSprites;
        Debug.Log("awake of selectecardsui");
    }

    private void UpdateSelectedCardsSprites(List<Sprite> a)
    {
        int temp = a.Count - 1;
        cardSpritePrefab.GetComponent<CardSpritePrefabScript>().cardSprite.sprite = a[temp];
        Instantiate(cardSpritePrefab, parent);
        Debug.Log("UpdateSelectedCardsSprites()");
    }
}
