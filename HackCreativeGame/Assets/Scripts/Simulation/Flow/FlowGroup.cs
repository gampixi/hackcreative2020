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
            FutureInfectedCount += count;
        }

        public void Infect()
        {
            if (FutureInfectedCount > population.healthy)
                FutureInfectedCount = population.healthy;
            population.healthy -= FutureInfectedCount;
            population.asymptomatic += FutureInfectedCount;
            FutureInfectedCount = 0;
        }

        public void PerformInternalChanges()
        {
            ProbabilityFlow(Settings.ImmunityLossProbability, ref population.recovered, ref population.healthy);
            ProbabilityFlow(Settings.SymptomaticProbability, ref population.asymptomatic, ref population.symptomatic);
            ProbabilityFlow(Settings.RecoverProbability, ref population.asymptomatic, ref population.recovered);
            ProbabilityFlow(Settings.RecoverProbability, ref population.symptomatic, ref population.recovered);
            ProbabilitySink(Settings.DeathProbability, ref population.symptomatic);
            ProbabilitySink(Settings.DeathProbability, ref population.asymptomatic);
        }
    }
}