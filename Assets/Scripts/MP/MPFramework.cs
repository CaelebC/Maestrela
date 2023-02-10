using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MPFramework is the 'basis' of the MP mechanics. MPManager actually uses the
// code here to make it work in game. Essentially the system making the MP work.

public class MPFramework
{
    private const float MAX = 75f;

    private float playerMP;
    public static float displayedMP;
    private float playerMaxMP;

    public MPFramework()
    {
        playerMP = 75f;  // BUG: Currently cannot grab the PlayerStats.MP variable
        playerMaxMP = MAX;
        displayedMP = Mathf.Floor(playerMP);
    }

    public bool TryUseMP(float drainAmount)
    {
        if (playerMP >= drainAmount)
        {
            playerMP -= drainAmount;
            displayedMP = Mathf.Floor(playerMP);
            Debug.Log("playerMP: " + playerMP);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckBurnout()
    {
        return !(this.playerMP >= 1f);
    }

    public void RecoverFromBurnout()
    {
        playerMP = playerMaxMP / 2f;
        displayedMP = Mathf.Floor(playerMP);
        Debug.Log("RECOVERFROMBURNOUT FUNCTION RAN");
    }
}
