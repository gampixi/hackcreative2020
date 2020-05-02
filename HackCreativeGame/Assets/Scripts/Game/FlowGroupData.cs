using Game.Simulation;

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
        public List<TransmitProbabilityMultiplier> transmitMultipliers;
        public FlowGroupData(FlowGroupKind target)
        {
            this.target = target;
        }

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
        }
    }
}    