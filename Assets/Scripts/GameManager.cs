using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    // public GameObject burnoutStateUI;

    // private MPFramework mpFramework;

    
    // private void Awake() 
    // {
    //     mpFramework = new MPFramework();
    // }
    
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

        // if (mpFramework.CheckBurnout())
        // {
        //     Burnout();
            
        // }
        // else
        //     return;
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    // void Burnout()
    // {
    //     burnoutStateUI.SetActive(true);
    // }
}
