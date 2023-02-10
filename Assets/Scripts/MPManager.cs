using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MPManager : MonoBehaviour
{   
    private MPFramework mpFramework;
    public bool burnout = false;
    
    private void Awake() 
    {
        mpFramework = new MPFramework();
    }
    
    private void Update() 
    {
        if (!burnout)
        {
            float rateMP = 10f;
            if (mpFramework.TryUseMP(rateMP * Time.deltaTime))
            {
                Debug.Log("DRAINING IS HAPPENING");
                // Continuously drains
            }
            else
            {
                burnout = true;
            }
        }
    }
}
