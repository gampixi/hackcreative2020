using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Simulation
{
    [CreateAssetMenu(fileName = "NewFlowGroupSettings", menuName = "Custom Game Data/Flow Group Settings", order = 1)]
    public class FlowGroupSettings : ScriptableObject
    {
        public FlowGroupKind kind;
        //[SerializeField]
        public List<TransmitSettings> transmitFlowSettings;

        [ContextMenu("Fill Flow Transmit Source")]
        public void FillSource()
        {
            transmitFlowSettings.ForEach(x => x.source = kind);
        }
        /*public List<TransmitSettings> TransmitFlowSettings
        {
            get
            {
                var list = transmitFlowSettings.Where(x => x.target == kind)
                    .ToList();
                foreach (var item in list)
                {
                    var transmitMultipliers = SimulationController.Instance.benefits
                        .GetOrSetFlowData(kind)
                        .transmitMultipliers;
                    foreach (var multipliers in transmitMultipliers)
                    {
                        item.transmitProbability *= multipliers.transmitMultiplier;
                    }
                }

                return list;
            }
        }*/


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
        public class TransmitSettings
        {
            [SerializeField]
            private float transmitProbability;
            public float impactFromHappiness; //Negative to boost when negative, positive to boost when positive
            public float TransmitProbability
            {
                get
                {
                    var happinessMultiplier = 1f;
                    if (impactFromHappiness > 0.000001f)
                    {
                        happinessMultiplier =
                            (float)(1f + SimulationController.Instance.happiness.happiness * impactFromHappiness);
                    }
                    else if (impactFromHappiness < -0.000001f)
                    {
                        happinessMultiplier =
                            (float)(1f + SimulationController.Instance.happiness.happiness * -impactFromHappiness);
                    }
                    //(float)(SimulationController.Instance.happiness.happiness >= 0f
                    //    ? 1f + SimulationController.Instance.happiness.happiness * impactFromHappiness
                    //    : 1f / (1 + -SimulationController.Instance.happiness.happiness * impactFromHappiness));
                    var transmitMultiplierData = SimulationController.Instance.benefits.GetOrSetFlowData(source)
                        .transmitMultipliers
                        .FirstOrDefault(x => x.target == target);
                    return ProbabilityMath
                        .ProbabilityMultiplier(transmitProbability,
                            (transmitMultiplierData?.transmitMultiplier ?? 1) * happinessMultiplier);
                }
            }

            public FlowGroupKind source;
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
            get { return symptomaticProbability * SimulationController.Instance.benefits.GetOrSetFlowData(kind).symptomaticMultiplier; }
        }
        [SerializeField]
        float recoverProbability;
        public float RecoverProbability
        {
            get { return recoverProbability * SimulationController.Instance.benefits.GetOrSetFlowData(kind).recoverMultiplier; }
        }
        [SerializeField]
        private float deathProbability;
        public float DeathProbability
        {
            get { return deathProbability * SimulationController.Instance.benefits.GetOrSetFlowData(kind).deathMultiplier; }
        }
        [SerializeField]
        private float travelProbability;
        public float TravelProbability
        {
            get { return travelProbability * SimulationController.Instance.benefits.GetOrSetFlowData(kind).travelMultiplier; }
        }
        [SerializeField]
        private float immunityLossProbability;
        public float ImmunityLossProbability
        {
            get { return immunityLossProbability * SimulationController.Instance.benefits.GetOrSetFlowData(kind).immunityLossMultiplier; }
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
            get { return happinessPerSymptomatic * SimulationController.Instance.benefits.GetOrSetFlowData(kind).unhappinessMultiplier; }
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
