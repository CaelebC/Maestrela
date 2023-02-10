using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    private string mainMenuScene = "MainMenu";
    public SceneFader sceneFader;

    void OnEnable() 
    {
        waveNumberText.text = "Waves Survived: " + PlayerStats.waves.ToString();
        Time.timeScale = 0f;
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(mainMenuScene);
    }
}
