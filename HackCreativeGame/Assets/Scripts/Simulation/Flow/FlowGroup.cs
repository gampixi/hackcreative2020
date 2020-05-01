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

        public static FlowGroup New(FlowGroupSettings settings, int regionPopulation)
        {
            return new FlowGroup
            {
                Settings = settings,
                population = new Population
                {
                    healthy = Mathf.RoundToInt((float) regionPopulation * settings.initialPopulationProportion)
                },
            };
        }

        public void PerformInternalChanges()
        {
            var toSymptomatic = Mathf.RoundToInt(population.asymptomatic * ValueForProbability(Settings.symptomaticProbability));
            var symptomaticToRecovered = Mathf.RoundToInt(population.symptomatic * ValueForProbability(Settings.recoverProbability));
            var asymptomaticToRecovered = Mathf.RoundToInt(
                (population.asymptomatic-toSymptomatic) * ValueForProbability(Settings.recoverProbability));
            population.healthy -= toSymptomatic;
            population.asymptomatic += toSymptomatic;
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