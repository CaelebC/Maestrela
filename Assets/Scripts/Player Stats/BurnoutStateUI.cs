using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BurnoutStateUI : MonoBehaviour
{
    public TextMeshProUGUI recoveryTimeText;
    public static float timeRemaining;
    private float startingTimeRemaining;
    public Image burnoutImage;
    
    void OnEnable() 
    {  
        startingTimeRemaining = MPManager.recoveryTimeDisplay;
        burnoutImage.canvasRenderer.SetAlpha(1f);
    }

    void Update()
    {
        burnoutImage.canvasRenderer.SetAlpha(MPManager.recoveryTimeDisplay/startingTimeRemaining);
        timeRemaining = Mathf.Floor(MPManager.recoveryTimeDisplay);
        recoveryTimeText.text = ("Recovery in: " + timeRemaining);
    }
}
