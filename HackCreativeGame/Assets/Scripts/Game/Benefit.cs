using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game.FlowGroupData;

namespace Game
{
    [CreateAssetMenu(fileName = "NewBenefitSettings", menuName = "Custom Game Data/Benefit Settings", order = 1)]
    public class Benefit : ScriptableObject
    {
        public string Title;
        public string Description;
        public float PricePerHealthy;
        public float PricePerKnownSick;
        public List<FlowGroupData> FlowData;
        public List<TransmitProbabilityMultiplier> transmitProbabilityMultipliers;
    }
}