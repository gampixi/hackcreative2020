﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    public class SimulationController : MonoBehaviour
    {
        public List<RegionNode> regions;

        private void Start()
        {
            regions.ForEach(x => x.InitializeRegion());
        }

        //public 
        
        [ContextMenu("Tick Forward")]
        public void TickForward()
        {
            // Calculations should be done before actually manipulating the data
            // so no weird side effects happen
            // Order should be kept: healing -> internal transmission -> travel
            regions.ForEach(x => x.population.ForEach(p =>
            {
                p.PerformInternalChanges();
                
            }));
        }
    }
}
