using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Simulation;

namespace Game
{
    [CreateAssetMenu(fileName = "NewBenefitSettings", menuName = "Custom Game Data/Benefit Settings", order = 1)]
    public class Benefit : ScriptableObject
    {
        public List<FlowGroupData> FlowData;

        [System.Serializable]
        public class FlowGroupData
        {
            public FlowGroupKind target;
            public float infectProbability = 1f;
            public float happinessMultiplier = 1f;
            public FlowGroupData(FlowGroupKind target)
            {
                this.target = target;
            }
        }
    }
}