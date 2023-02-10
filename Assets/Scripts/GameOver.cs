using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;

    void OnEnable() 
    {
        waveNumberText.text = "Waves Survived: " + PlayerStats.waves.ToString();
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Debug.Log("quit to menu");
    }
}
