using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    [CreateAssetMenu(fileName = "NewRegionSettings", menuName = "Custom Game Data/Region Settings", order = 1)]
    public class RegionSettings : ScriptableObject
    {
        public List<FlowGroupSettings> regionFlowGroups;

        
        
        public int initialPopulation;
    }
}

