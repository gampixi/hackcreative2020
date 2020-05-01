using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Simulation
{
    public class RegionNode : MonoBehaviour
    {
        public RegionSettings settings;
        public List<FlowGroup> population;
        public List<RegionNode> neighbors;

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

        public void InitializeRegion()
        {
            population = settings.InitialFlows();
        }
    }
}

