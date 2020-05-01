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
        public int Infected => population.symptomatic + population.asymptomatic;
        private int FutureInfectedCount = 0;

        public FlowGroup(FlowGroupSettings settings, int regionPopulation)
        {
            Settings = settings;
            population = new Population
            {
                healthy = Mathf.RoundToInt(regionPopulation * settings.initialPopulationProportion) - 1,
                asymptomatic = 1
                //FIXME
            };
        }

        public void CalculateFutureInfected(int count)
        {
            FutureInfectedCount += (count > population.healthy) 
                ? population.healthy
                : count;
        }

        public void Infect()
        {
            population.healthy -= FutureInfectedCount;
            population.asymptomatic += FutureInfectedCount;
            FutureInfectedCount = 0;
        }

        public void PerformInternalChanges()
        {
            ProbabilityFlow(Settings.immunityLossProbability, ref population.recovered, ref population.healthy);
            ProbabilityFlow(Settings.symptomaticProbability, ref population.asymptomatic, ref population.symptomatic);
            ProbabilityFlow(Settings.recoverProbability, ref population.asymptomatic, ref population.recovered);
            ProbabilityFlow(Settings.recoverProbability, ref population.symptomatic, ref population.recovered);
            ProbabilitySink(Settings.deathProbability, ref population.symptomatic);
            ProbabilitySink(Settings.deathProbability, ref population.asymptomatic);
        }
    }
}