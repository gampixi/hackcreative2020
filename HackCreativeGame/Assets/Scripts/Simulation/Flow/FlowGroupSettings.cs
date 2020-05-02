using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Simulation
{
    [CreateAssetMenu(fileName = "NewFlowGroupSettings", menuName = "Custom Game Data/Flow Group Settings", order = 1)]
    public class FlowGroupSettings : ScriptableObject
    {
        public FlowGroupKind kind;
        public List<TransmitFlowSettings> transmitFlowSettings;


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

        [System.Serializable]
        public class TransmitFlowSettings
        {
            [SerializeField]
            public float transmitProbability;

            public FlowGroupKind target;

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

        [SerializeField]
        private float happinessPerCapita;
        
        public float HappinessPerCapita
        {
            get { return happinessPerCapita * SimulationController.Instance.benefits.GetOrSetFlowData(kind).happinessMultiplier; }
        }
        [SerializeField]
        private float happinessPerSymptomatic;
        public float HappinessPerSymptomatic
        {
            get { return happinessPerSymptomatic * SimulationController.Instance.benefits.GetOrSetFlowData(kind).happinessMultiplier; }
        }

        [SerializeField]
        private float taxPerTurn;

        public float TaxPerTurn
        {
            get { return taxPerTurn * SimulationController.Instance.benefits.GetOrSetFlowData(kind).taxMultiplier; }
        }
        
        [SerializeField]
        private float taxPerTurnSymptomatic;

        public float TaxPerTurnSymptomatic
        {
            get { return taxPerTurnSymptomatic * SimulationController.Instance.benefits.GetOrSetFlowData(kind).taxMultiplier; }
        }
    }

    public enum FlowGroupKind
    {
        Worker,
        Unemployed,
        Anarchist,
        Medic,
        Police,
        Wealthy,
        Students,
        Retired,
    }
}
