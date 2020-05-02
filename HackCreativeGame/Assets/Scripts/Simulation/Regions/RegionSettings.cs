using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Simulation
{
    [CreateAssetMenu(fileName = "NewRegionSettings", menuName = "Custom Game Data/Region Settings", order = 1)]
    public class RegionSettings : ScriptableObject
    {
        public List<FlowGroupSettings> regionFlowGroups;
        public int initialPopulation;

        public List<FlowGroup> InitialFlows()
        {
            var proprtionSum = 0f;
            var pop = new List<FlowGroup>();
            regionFlowGroups.ForEach(x => proprtionSum += x.initialPopulationProportion);
            regionFlowGroups.ForEach(x => pop.Add(new FlowGroup(x, Mathf.RoundToInt((float)initialPopulation * (x.initialPopulationProportion/proprtionSum)))));
            return pop;
        }
    }
}

