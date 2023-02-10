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

    private float rateMP;
    public float defaultRecoveryTime;
    private float recoveryTime;
    public static float recoveryTimeDisplay;

    // public GameObject ui;
    
    private void Awake() 
    {
        mpFramework = new MPFramework();
        recoveryTime = defaultRecoveryTime;
    }
    
    private void Update() 
    {
        if (mpFramework.CheckBurnout())  // Player in burnout
        {
            burnoutStateUI.SetActive(true);
            if (recoveryTime <= 0f)
            {
                StartCoroutine(Recover());
                recoveryTime = defaultRecoveryTime;
            }
            
            recoveryTime -= Time.deltaTime;
            recoveryTime = Mathf.Clamp(recoveryTime, 0f, Mathf.Infinity);
            recoveryTimeDisplay = recoveryTime;

        }
        else  // Continuously draining
        {   
            rateMP = PlayerStats.drainRateMP;
            if (mpFramework.TryUseMP(rateMP * Time.deltaTime))
            {
                // Debug.Log("DRAINING IS HAPPENING");
            }
        }

    }

    IEnumerator Recover()  // IEnumerator and Coroutines were used to prevent bugs from happening when recovering MP.
    {
        burnoutStateUI.SetActive(false);
        mpFramework.RecoverFromBurnout();
        Debug.Log("Recover() function is being called");
        yield return new WaitForSeconds(10f);
    }
}
