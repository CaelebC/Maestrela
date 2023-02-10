using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TP;
    public int startingTP = 50;
    public static int HP;
    public int startingHP;

    void Start()
    {
        TP = startingTP;
        HP = startingHP;
    }
}
