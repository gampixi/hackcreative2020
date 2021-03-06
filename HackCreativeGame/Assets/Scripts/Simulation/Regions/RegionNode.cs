using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Game.Utilities.RandomExtensions;

namespace Game.Simulation
{
    public class RegionNode : MonoBehaviour
    {
        public string regionName = "Coney Island";
        public RegionSettings settings;
        public List<FlowGroup> population;
        public List<RegionNode> neighbors;
        public Population TotalPopulation { get; private set; }

        private void Start()
        {
            TotalPopulation = new Population();
            UpdateTotalPopulation();
        }

        public void InfectRandomly(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var group = population[Random.Range(0, population.Count)];
                group.CalculateFutureInfected(2);
                group.Infect();
            }
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
                        var possibleInfectedPeopleCount = probability.TransmitProbability 
                            * group.Infected 
                            * (1f+Mathf.Log(group.population.healthy+1f, 15));
                        //var possibleInfectedPeopleCount = probability.TransmitProbability * group.Infected;
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

        public void PerformInternalFlowChange()
        {
            foreach (var flowGroup in population)
            {
                foreach (var item in flowGroup.Settings.transmitFlowSettings)
                {
                    var targetGroup = population.FirstOrDefault(x => x.Settings.kind == item.target);
                    var travelers = new Population
                    {
                        healthy = Mathf.RoundToInt(flowGroup.population.healthy *
                                                  ValueForProbability(item.TransmitProbability, 2)),
                        asymptomatic = Mathf.RoundToInt(flowGroup.population.asymptomatic *
                                                       ValueForProbability(item.TransmitProbability, 2)),
                        recovered = Mathf.RoundToInt(flowGroup.population.recovered *
                                                    ValueForProbability(item.TransmitProbability, 2))
                    };

                    flowGroup.population -= travelers;
                    targetGroup.population += travelers;
                }
            }
        }

        public void PerformTravel()
        {
            var travelDensitySum = neighbors.Sum(x => x.settings.TravelDensity);
            foreach (var group in population)
            {
                var travelers = new Population
                {
                    healthy = Mathf.RoundToInt(@group.population.healthy *
                                               ValueForProbability(@group.Settings.TravelProbability, 2)),
                    asymptomatic = Mathf.RoundToInt(@group.population.asymptomatic *
                                                    ValueForProbability(@group.Settings.TravelProbability, 2)),
                    recovered = Mathf.RoundToInt(@group.population.recovered *
                                                 ValueForProbability(@group.Settings.TravelProbability, 2))
                };
                foreach (var neighbor in neighbors)
                {
                    var neighborGroup = neighbor.population.FirstOrDefault(x => x.Settings.kind == group.Settings.kind);
                    var travelersToThis = travelers * (neighbor.settings.TravelDensity / travelDensitySum);
                    neighborGroup.population += travelersToThis;
                    group.population -= travelersToThis;
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

