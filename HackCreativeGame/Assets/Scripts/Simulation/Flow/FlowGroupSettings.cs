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
            [SerializeField]
            private float transmitProbability;
            public float TransmitProbability => ProbabilityMath
                .ProbabilityMultiplier(transmitProbability, 
                    SimulationController.Instance.benefits.GetOrSetFlowData(target).infectProbability);
        }

        public float initialPopulationProportion;
        
        public List<TransmitData> transmitProbabilities;
        [SerializeField]
        private float symptomaticProbability;
        public float SymptomaticProbability
        {
            // For now they are simple getters, but in the future
            // the getters might ask BenefitProvider what multipliers to apply
            get { return symptomaticProbability; }
        }
        [SerializeField]
        float recoverProbability;
        public float RecoverProbability
        {
            get { return recoverProbability; }
        }
        [SerializeField]
        private float deathProbability;
        public float DeathProbability
        {
            get { return deathProbability; }
        }
        [SerializeField]
        private float travelProbability;
        public float TravelProbability
        {
            get { return travelProbability; }
        }
        [SerializeField]
        private float immunityLossProbability;
        public float ImmunityLossProbability
        {
            get { return immunityLossProbability; }
        }
    }

    public enum FlowGroupKind
    {
        Worker,
        Unemployed,
        Anarchist
    }
}
