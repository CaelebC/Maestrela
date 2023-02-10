using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public string nextSceneName;

    void Start()
    {
        GameIsOver = false;
    }
    
    void Update()
    {
        if (GameIsOver)
            return;

        if(PlayerStats.HP <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        levelCompleteUI.SetActive(true);
        Debug.Log("ALL WAVES COMPLETED");
    }
}
