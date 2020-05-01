using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    [CreateAssetMenu(fileName = "NewFlowGroupSettings", menuName = "Custom Game Data/Flow Group Settings", order = 1)]
    public class FlowGroupSettings : ScriptableObject
    {
        public FlowGroupKind kind;

        [System.Serializable]
        public class TransmitData
        {
            public FlowGroupKind target;
            public float transmitProbability;
        }

        public List<TransmitData> transmitProbabilities;
        public float symptomaticProbability;
        public float recoverProbability;
        public float deathProbability;
        public float initialPopulationProportion;
        public float travelProbability;
        public float immunityLossProbability;
    }

    public enum FlowGroupKind
    {
        Worker,
        Unemployed,
        Anarchist
    }
}
