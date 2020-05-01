using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Utilities
{
    public static class RandomExtensions
    {
        public static float ValueForProbability(float probability)
        {
            return (probability != 0)
                ? Random.Range(Mathf.Pow(probability, 2), (float)Math.Sqrt(probability))
                : 0;
        }
    }
}

