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
        Time.timeScale = 0f;
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Debug.Log("quit to menu");
    }
}
