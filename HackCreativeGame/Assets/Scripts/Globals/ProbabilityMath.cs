using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProbabilityMath
{
    public static float ProbabilityMultiplier(float p, float m)
    {
        //Atrisina 0.8 varbutiba klust 2x lielaka = ?
        return 1 + ((1 - p) / -((p * m) + 1 - p));
    }
}
