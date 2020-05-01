using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Simulation;

namespace Game
{
    [CreateAssetMenu(fileName = "NewBenefitSettings", menuName = "Custom Game Data/Benefit Settings", order = 1)]
    public class BenefitSettings : ScriptableObject
    {
        [System.Serializable]
        public class HappinessData
        {
            public FlowGroupKind target;
            public float happinessMultiplier;
        }
        
        public List<HappinessData> happinessData;
        
        [System.Serializable]
        public class InfectProbabilityData
        {
            public FlowGroupKind target;
            public float infectProbabilityMultiplier;
        }
        
        public List<InfectProbabilityData> infectProbabilityData;
    }
}