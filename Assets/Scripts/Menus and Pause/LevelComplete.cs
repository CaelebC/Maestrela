using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    private string mainMenuScene = "MainMenu";
    private string nextLevelScene = "Ch2_SecondSemester";
    public SceneFader sceneFader;

    void OnEnable() 
    {
        waveNumberText.text = "Waves Survived: " + PlayerStats.waves.ToString();
        Time.timeScale = 0f;
    }
    
    public void NextLevel()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        sceneFader.FadeTo(nextLevelScene);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        sceneFader.FadeTo(mainMenuScene);
    }
}
