using System;
using Game.Simulation;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class FlowGroupData
    {
        public FlowGroupKind target;
        public float infectProbability = 1f;
        public float happinessMultiplier = 1f;
        public float unhappinessMultiplier = 1f;
        public float taxMultiplier = 1f;
        public float symptomaticMultiplier = 1f;
        public float deathMultiplier = 1f;
        public float recoverMultiplier = 1f;
        public float travelMultiplier = 1f;
        public float immunityLossMultiplier = 1f;
        public List<TransmitProbabilityMultiplier> transmitMultipliers = new List<TransmitProbabilityMultiplier>();
        public FlowGroupData(FlowGroupKind target)
        {
            this.target = target;
            this.Reset();
        }

        [System.Serializable]
        public class TransmitProbabilityMultiplier
        {
            public FlowGroupKind target;
            public float transmitMultiplier;
        }

        public void Reset()
        {
            infectProbability = 1f;
            happinessMultiplier = 1f;
            unhappinessMultiplier = 1f;
            taxMultiplier = 1f;
            symptomaticMultiplier = 1f;
            deathMultiplier = 1f;
            recoverMultiplier = 1f;
            travelMultiplier = 1f;
            immunityLossMultiplier = 1f;
            transmitMultipliers.Clear();
            foreach (var kind in (FlowGroupKind[]) Enum.GetValues(typeof(FlowGroupKind)))
            {
                transmitMultipliers.Add(new TransmitProbabilityMultiplier
                {
                    target = kind,
                    transmitMultiplier = 1
                });
            }
        }
    }
}    