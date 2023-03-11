using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Writing,      // 0
    Electronics,  // 1
    Tools,        // 2
    Necessities,  // 3
    BossMob,      // 4
    MPTower,      // 5, this is just for consistency and isn't supposed to be used
}

public class TypeMatchup
{
    private static float md = 1.2f;
    private static float d = 1f;
    private static float ld = 0.8f;
    
    static float[][] chart =
    {
        // Each row is attack type
        // Floats in {} (columns) is the mob's type

        // {Writing, Electronics, Tools, Necessities, BossMob, MPTower}

        new float[] {md, ld, ld, 0f, d, 0f},        // Writing Tower
        new float[] {d, md, d, 0f, d, 0f},          // Electronics Tower
        new float[] {ld, d, md, 0f, d, 0f},         // Tools Tower
        new float[] {0f, 0f, 0f, d, 0f, 0f},        // Necessities Tower
        new float[] {d, d, d, 0f, d, 0f},           // BossMob Tower (should never be used, but is here for consistency)
        new float[] {0f, 0f, 0f, 0f, 0f, 0f},       // MPTower Tower (should never be used, but is here for consistency)
    };

    public static float GetEffectiveness(EntityType enemyType, EntityType projectileType)
    {
        int row = (int)projectileType;
        int col = (int)enemyType;
        // Debug.Log("row:" + row + " | col:" + col);
        return chart[row][col];
    }
}