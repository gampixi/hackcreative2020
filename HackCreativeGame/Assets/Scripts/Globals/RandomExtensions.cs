using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Utilities
{
    public static class RandomExtensions
    {
        public static float ValueForProbability(float probability, float power = 5)
        {
            return (probability > 0.00001f)
                ? Random.Range(Mathf.Pow(probability, power), (float)Math.Pow(probability, 1f/power))
                : 0;
        }

        public static void ProbabilityFlow(float probability, ref int source, ref int target)
        {
            var flowAmount = Mathf.RoundToInt(source * ValueForProbability(probability, 2));
            if (flowAmount < 1)
            {
                if (Random.value < 0.1f)
                {
                    flowAmount = 1;
                }
            }

            if (flowAmount > source)
                flowAmount = source;

            source -= flowAmount;
            target += flowAmount;
        }
        
        public static void ProbabilitySink(float probability, ref int source, float smallValueProbability = 0.1f)
        {
            var flowAmount = Mathf.RoundToInt(source * ValueForProbability(probability, 2));
            if (flowAmount < 1)
            {
                if (Random.value < smallValueProbability)
                {
                    flowAmount = 1;
                }
            }
            
            if (flowAmount > source)
                flowAmount = source;

            source -= flowAmount;
        }
    }
}

