using Game.Simulation;

namespace Game
{
    [System.Serializable]
    public class FlowGroupData
    {
        public FlowGroupKind target;
        public float infectProbability = 1f;
        public float happinessMultiplier = 1f;
        public float taxMultiplier = 1f;
        public FlowGroupData(FlowGroupKind target)
        {
            this.target = target;
        }

        public void Reset()
        {
            infectProbability = 1f;
            happinessMultiplier = 1f;
            taxMultiplier = 1f;
        }
    }
}    