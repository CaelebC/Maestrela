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
        timeRemaining = (startingTimeRemaining - Time.deltaTime);
        
        recoveryTimeText.text = "Recoverry in: " + (timeRemaining).ToString();
        Time.timeScale = 1f;
        // Invoke("Recovered", 10f);
        // if (timeUntilRecovery() == 0f)
        // {
        //     this.SetActive(false);
        // }
        // insert code to make burnout recover after 10 seconds



        // Insert code to reduce all placed towers' damage output.
    }

    // void Recovered()
    // {
    //     gameObject.SetActive(false);
    // }

    private void OnDisable() 
    {
        Debug.Log("Disabled");
    }

    // private void Update() 
    // {
    //     timeRemaining = startingTimeRemaining;
    // }
}
