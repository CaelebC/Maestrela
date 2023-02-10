using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int TP;
    public int startTP = 50;

    void Start()
    {
        TP = startTP;
    }
}
