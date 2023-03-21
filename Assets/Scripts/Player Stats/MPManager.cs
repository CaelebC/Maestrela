using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MPManager : MonoBehaviour
{   
    public static event Action<float> OnBurnout;
    public static event Action<float> OnRecover;
    
    [SerializeField]
    private GameObject burnoutStateUI;

    public float defaultRecoveryTime;
    private float recoveryTime;
    public static float recoveryTimeDisplay;

    
    private void Start() 
    {        
        recoveryTime = defaultRecoveryTime;
        recoveryTimeDisplay = recoveryTime;
    }
    
    private void Update() 
    {
        if (CheckBurnout())  // Player in burnout
        {
            burnoutStateUI.SetActive(true);
            OnBurnout?.Invoke(PlayerStats.BurnoutDamageReduction);

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
            if (TryUseMP(PlayerStats.drainRateMP * Time.deltaTime))
            {
                burnoutStateUI.SetActive(false);
                // Debug.Log("DRAINING IS HAPPENING");
            }
        }

    }

    IEnumerator Recover()  // IEnumerator and Coroutines were used to prevent bugs from happening when recovering MP.
    {
        burnoutStateUI.SetActive(false);
        RecoverFromBurnout();
        // Debug.Log("Recover() function is being called");
        yield return new WaitForSeconds(10f);
    }

    public bool TryUseMP(float drainAmount)
    {
        if (PlayerStats.MP >= drainAmount)
        {
            PlayerStats.MP -= drainAmount;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Returns false if player still has MP >= 1
    public bool CheckBurnout()
    {
        return !(PlayerStats.MP >= 1f);
    }

    public void RecoverFromBurnout()
    {
        PlayerStats.MP = PlayerStats.maxMP / 2f;
        OnRecover?.Invoke(1f);
        // Debug.Log("RECOVERFROMBURNOUT FUNCTION RAN");
    }

    public static void RecoverMP(float amount)
    {
        PlayerStats.MP += amount;
        // Debug.Log("MP RECOVERED");
    }

}
