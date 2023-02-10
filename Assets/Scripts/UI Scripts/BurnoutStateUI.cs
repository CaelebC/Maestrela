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
        recoveryTimeText.text = "Recoverry in: " + (MPManager.recoveryTimeDisplay).ToString();
        Time.timeScale = 1f;
    }

    private void OnDisable() 
    {
        Debug.Log("Disabled");
    }
}
