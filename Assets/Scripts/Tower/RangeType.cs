using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeType
{
    NoRange=0,
    VeryShort=7,
    Short=12,
    Medium=18,
    Long=24,
}

public class RangeText
{
    public static string GetRange(float _range)
    {
        if (_range == (float)RangeType.VeryShort) { return "Very Short"; }
        if (_range == (float)RangeType.Short) { return "Short"; }
        if (_range == (float)RangeType.Medium) { return "Medium"; }
        if (_range == (float)RangeType.Long) { return "Long"; }
        return "No Range";
    }
}
