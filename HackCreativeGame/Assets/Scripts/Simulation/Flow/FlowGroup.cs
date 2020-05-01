using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game.Utilities.RandomExtensions;

namespace Game.Simulation
{
    public class FlowGroup
    {
        public FlowGroupSettings Settings { get; private set; }
        public Population population;
        private Population _populationTransition;
        public int Infected => population.symptomatic + population.asymptomatic;

        public static FlowGroup New(FlowGroupSettings settings, int regionPopulation)
        {
            var fg = new FlowGroup();
            fg.Settings = settings;
            fg.population = new Population
            {
                healthy = Mathf.RoundToInt((float) regionPopulation * settings.initialPopulationProportion)
            };
            fg._populationTransition = new Population();
            return fg;
        }

        public void CalculateInternalChanges()
        {
            var toSymptomatic = Mathf.RoundToInt(population.asymptomatic * ValueForProbability(Settings.symptomaticProbability));
            var symptomaticToRecovered = Mathf.RoundToInt(population.symptomatic * ValueForProbability(Settings.recoverProbability));
            var asymptomaticToRecovered = Mathf.RoundToInt(
                (population.asymptomatic-population.symptomatic) * ValueForProbability(Settings.recoverProbability));
            //var 
        }
    }
}