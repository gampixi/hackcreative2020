using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Simulation
{
    public class FlowGroup
    {
        public FlowGroupSettings Settings { get; private set; }
        public int population;

        public static FlowGroup Initialize(FlowGroupSettings settings, int regionPopulation)
        {
            var fg = new FlowGroup();
            fg.Settings = settings;
            fg.population = Mathf.RoundToInt((float) regionPopulation * settings.initialPopulationProportion);
            return fg;
        }
    }
}