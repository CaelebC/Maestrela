using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour
{
    public static Loadout loadoutInstance;

    [SerializeField] int loadoutCount;
    static int lc = 8;
    public static int LoadoutCount { get{return lc;} }
    
    public List<Tower> allTowers;
    public List<Tower> defaultLoadout;

    [HideInInspector] public static List<Tower> savedLoadout = new List<Tower>();

    void Awake()
    {
        loadoutInstance = this;
        lc = loadoutCount;
    }
}
