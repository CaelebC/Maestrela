using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardSO selectedCard;
    
    void Update()
    {
        selectedCard.ApplyCard();
        Debug.Log("Card applied");
    }

    
}
