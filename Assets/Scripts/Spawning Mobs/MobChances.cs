using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobChances
{
    // for debug :
    public string mobName;

    public GameObject Prefab;
    [Range (0f, 100f)]public float Chance = 100f;

    [HideInInspector] public double _weight;
}
