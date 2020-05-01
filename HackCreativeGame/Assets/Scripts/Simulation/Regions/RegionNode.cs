using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

