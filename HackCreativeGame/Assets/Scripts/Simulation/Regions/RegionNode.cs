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
        public Population TotalPopulation { get; private set; }

        private void Start()
        {
            TotalPopulation = new Population();
        }

        public void PerformInternalTransmission()
        {
            
        }

        public void UpdateTotalPopulation()
        {
            TotalPopulation.Reset();
            population.ForEach(x => TotalPopulation += x.population);
        }
        
        public void InitializeRegion()
        {
            population = settings.InitialFlows();
        }
    }
}

