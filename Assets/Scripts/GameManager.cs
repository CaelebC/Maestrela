using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;

    void Update()
    {
        if (gameEnded)
            return;

        if(PlayerStats.HP <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("GAMEOVER");
    }
}
