using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Game.Utilities.RandomExtensions;

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
            UpdateTotalPopulation();
        }

        public void PerformInternalTransmission()
        {
            foreach (var group in population)
            {
                foreach (var probability in group.Settings.transmitProbabilities)
                {
                    var infectedCount = 0;
                    var targetGroup = population.FirstOrDefault(x => x.Settings.kind == probability.target);
                    if (targetGroup != null)
                    {
                        var possibleInfectedPeopleCount = probability.transmitProbability * group.Infected;
                        var range = Random.Range(0, possibleInfectedPeopleCount);
                        if (possibleInfectedPeopleCount < 1)
                        {
                            if (Random.value < range)
                            {
                                infectedCount = 1;
                            }
                        }
                        else
                        {
                            infectedCount = Mathf.FloorToInt(range);
                        }
                    }

                    group.CalculateFutureInfected(infectedCount);
                }
            }
            population.ForEach(x => x.Infect());
        }

        public void PerformTravel()
        {
            foreach (var group in population)
            {
                var travelers = new Population
                {
                    healthy = Mathf.RoundToInt(@group.population.healthy *
                                               ValueForProbability(@group.Settings.travelProbability)),
                    asymptomatic = Mathf.RoundToInt(@group.population.asymptomatic *
                                                    ValueForProbability(@group.Settings.travelProbability)),
                    recovered = Mathf.RoundToInt(@group.population.recovered *
                                                 ValueForProbability(@group.Settings.travelProbability))
                };
                var travelersPerNeighbor = travelers * (1f / neighbors.Count);
                // Šobrīd pieņemam ka galamērķu sadalījums ir konstants
                foreach (var neighbor in neighbors)
                {
                    Debug.Log($"From {gameObject.name} to {neighbor.gameObject.name} with {travelersPerNeighbor}");
                    var neighborGroup = neighbor.population.FirstOrDefault(x => x.Settings.kind == group.Settings.kind);
                    neighborGroup.population += travelersPerNeighbor;
                    group.population -= travelersPerNeighbor;
                }
            }
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

