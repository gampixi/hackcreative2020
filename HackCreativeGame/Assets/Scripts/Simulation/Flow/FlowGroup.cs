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
            var toSymptomatic = Mathf.RoundToInt(population.asymptomatic * ValueForProbability(Settings.symptomaticProbability));
            var symptomaticToRecovered = Mathf.RoundToInt(population.symptomatic * ValueForProbability(Settings.recoverProbability));
            var asymptomaticToRecovered = Mathf.RoundToInt(
                (population.asymptomatic-toSymptomatic) * ValueForProbability(Settings.recoverProbability));
            population.asymptomatic -= toSymptomatic;
            population.symptomatic += toSymptomatic;
            population.symptomatic -= symptomaticToRecovered;
            population.recovered += symptomaticToRecovered;
            population.asymptomatic -= asymptomaticToRecovered;
            population.recovered += asymptomaticToRecovered;
            var symtopmaticDied = Mathf.RoundToInt(population.symptomatic * ValueForProbability(Settings.deathProbability));
            var asymtopmaticDied = Mathf.RoundToInt(population.asymptomatic * ValueForProbability(Settings.deathProbability));
            population.symptomatic -= symtopmaticDied;
            population.asymptomatic -= asymtopmaticDied;
        }
    }
}