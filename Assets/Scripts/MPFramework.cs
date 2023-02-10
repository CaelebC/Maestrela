using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPFramework
{
    private const float MAX = 100f;

    private float playerMP;
    public static float displayedMP;
    private float playerMaxMP;

    public MPFramework()
    {
        playerMP = 1000f;  // BUG: Currently cannot grab the PlayerStats.MP variable
        displayedMP = playerMP;
        playerMaxMP = MAX;
    }

    public bool TryUseMP(float drainAmount)
    {
        if (playerMP >= drainAmount)
        {
            playerMP -= drainAmount;
            displayedMP = playerMP;
            Debug.Log("playerMP: " + playerMP);
            return true;
        }
        else
        {
            return false;
        }
    }
}
