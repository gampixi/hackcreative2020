using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    public class RegionNode : MonoBehaviour
    {
        public RegionSettings settings;
        public List<FlowGroup> population;
        public List<RegionNode> neighbors;

        public void InitializeRegion()
        {
            population = settings.InitialFlows();
        }
    }
}

