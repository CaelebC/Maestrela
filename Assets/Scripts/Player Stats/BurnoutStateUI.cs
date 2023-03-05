using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BurnoutStateUI : MonoBehaviour
{
    public TextMeshProUGUI recoveryTimeText;
    public static float timeRemaining;
    public float startingTimeRemaining;
    
    void OnEnable() 
    {  

    }

    void Update()
    {
        timeRemaining = Mathf.Floor(MPManager.recoveryTimeDisplay);
        recoveryTimeText.text = ("Recoverry in: " + timeRemaining);
    }
}
