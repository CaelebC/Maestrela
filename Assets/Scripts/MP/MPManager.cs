using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// MPManager is the file that utilizes MPFramework and does calculations and 'commands'
// with the MP. 

public class MPManager : MonoBehaviour
{   
    private MPFramework mpFramework;
    public GameObject burnoutStateUI;

    private float rateMP = 10f;

    // public GameObject ui;
    
    private void Awake() 
    {
        mpFramework = new MPFramework();
    }
    
    private void Update() 
    {
        if (mpFramework.CheckBurnout())  // Player in burnout
        {
            // Debug.Log("PLAYER IS IN BURNOUT");
            burnoutStateUI.SetActive(true);
            Invoke("Recover", 10f);
                
        }
        else  // Continuously draining
        {   
            if (mpFramework.TryUseMP(rateMP * Time.deltaTime))
            {
                // Debug.Log("DRAINING IS HAPPENING");
            }
        }

    }

    public void Recover()
    {
        burnoutStateUI.SetActive(false);
        mpFramework.RecoverFromBurnout();
    }
}
